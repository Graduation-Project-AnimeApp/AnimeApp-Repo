using AnimeFlix.Data;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Application.Mapping;
using AnimeFlixBackend.Application.Services;
using AnimeFlixBackend.Infrastructure.External;
using AnimeFlixBackend.Infrastructure.Persistence;
using Google.GenAI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

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
    options.AddPolicy("AllowAllDev", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// -------------------- Build --------------------
var app = builder.Build();

app.UseCors("AllowAllDev");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
