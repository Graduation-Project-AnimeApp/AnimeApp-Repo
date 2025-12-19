using AnimeFlix.Data;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Application.Mapping;
using AnimeFlixBackend.Application.Mapping.Services;
using AnimeFlixBackend.Application.Services;
using AnimeFlixBackend.Domain.Entities;
using AnimeFlixBackend.Infrastructure.External;
using AnimeFlixBackend.Infrastructure.Persistence;
using Google.GenAI;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// -------------------- 1. Authentication --------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
    };
});

// -------------------- 2. Controllers & JSON --------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Fix for potential 500 errors caused by circular references in Entities
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// -------------------- 3. Database & Infrastructure --------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// -------------------- 4. External APIs (Jikan & Gemini) --------------------
builder.Services.AddHttpClient<IJikanService, JikanService>(client =>
{
    client.BaseAddress = new Uri("https://api.jikan.moe/v4/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Google Gemini Client
builder.Services.AddSingleton(new Client(apiKey: builder.Configuration["GoogleAI:ApiKey"]));

// -------------------- 5. Application Services --------------------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<IWatchlistService, WatchlistService>();
builder.Services.AddScoped<IWatchHistoryService, WatchHistoryService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<IRecommendationEngine, SimpleRecommendationEngine>();
builder.Services.AddScoped<ICharacterChatService, CharacterChatService>();
builder.Services.AddScoped<IReviewSummarizerService, ReviewSummarizerService>();

// -------------------- 6. AutoMapper --------------------
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

// -------------------- 7. Hangfire --------------------
builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseMemoryStorage());

builder.Services.AddHangfireServer();

// -------------------- 8. Swagger --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnimeFlix API", Version = "v1" });

    // Fix for 500 errors caused by duplicate DTO names in different namespaces
    c.CustomSchemaIds(type => type.FullName);
});

// -------------------- 9. CORS --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// -------------------- 10. Middleware Pipeline --------------------

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Shows detailed errors for 500 status
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the exact name defined in the Policy above
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// Hangfire Dashboard (useful for monitoring AI progress)
app.UseHangfireDashboard();

app.MapControllers();

app.Run();