using AnimeFlix.Data;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Application.Mapping;
using AnimeFlixBackend.Application.Mapping.Services;
using AnimeFlixBackend.Application.Services;
using AnimeFlixBackend.Domain.Entities;
using AnimeFlixBackend.Infrastructure.External;
using AnimeFlixBackend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System;
using System.Net.Http;
using System.Text;
// Add controllers support
using Google.GenAI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// --- Services Configuration ---

// Add JWT Authentication
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


// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<AuthService>();



// -------------------- Controllers --------------------
builder.Services.AddControllers();

// -------------------- Database --------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------- Unit Of Work --------------------
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// -------------------- Jikan API --------------------
builder.Services.AddHttpClient<IJikanService, JikanService>(client =>
{
    client.BaseAddress = new Uri("https://api.jikan.moe/v4/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// -------------------- Application Services --------------------
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<IWatchlistService, WatchlistService>();
builder.Services.AddScoped<IWatchHistoryService, WatchHistoryService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<IRecommendationEngine, SimpleRecommendationEngine>();

// -------------------- AutoMapper --------------------
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

// -------------------- Google Gemini --------------------
//var apiKey = builder.Configuration["GoogleAI:ApiKey"];

//// ✅ SINGLE, CORRECT registration
//builder.Services.AddSingleton<Client>(_ =>
//    new Client(apiKey: apiKey)
//);
//builder.Services.AddHttpClient<ICharacterChatService, CharacterChatService>(client =>
//{
//    client.BaseAddress = new Uri("http://localhost:11434");
//});

//builder.Services.AddScoped<ICharacterChatService, CharacterChatService>();
// Register the API Client
builder.Services.AddSingleton(new Client(apiKey: builder.Configuration["GoogleAI:ApiKey"]));

// Register your Chat Service implementation
builder.Services.AddScoped<ICharacterChatService, CharacterChatService>();
// -------------------- Swagger --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AnimeFlix API",
        Version = "v1"
    });
});

// -------------------- CORS --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin() // Allows requests from any domain/port
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// --- Build Application ---
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();
// Note: Authentication middleware is typically required here for authorized endpoints (removed as requested)
app.UseAuthorization(); // This middleware is still required to enforce any [Authorize] attributes

// Maps controller endpoints to routing
app.MapControllers();

app.Run();
