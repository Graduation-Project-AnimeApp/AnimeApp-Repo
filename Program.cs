using AnimeFlix.Data;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Application.Mapping;
using AnimeFlixBackend.Application.Services;
using AnimeFlixBackend.Infrastructure.External;
using AnimeFlixBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using System;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// --- Services Configuration ---

// Add controllers support
builder.Services.AddControllers();

// Configure DbContext (Assuming AppDbContext is in AnimeFlix.Data)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Data Access and External Services ---
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register JikanService with HttpClient configuration
builder.Services.AddHttpClient<IJikanService, JikanService>(client =>
{
    // Base URL for Jikan API v4
    client.BaseAddress = new Uri("https://api.jikan.moe/v4/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// --- Application Service Layer ---
// Register all core application services and the recommendation engine
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<IWatchlistService, WatchlistService>();
builder.Services.AddScoped<IWatchHistoryService, WatchHistoryService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
// FIX: Register the concrete implementation for the engine interface
builder.Services.AddScoped<IRecommendationEngine, SimpleRecommendationEngine>();

// --- AutoMapper Registration ---
builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); });

// --- Swagger/OpenAPI Configuration ---
// Adds services to generate the OpenAPI specification
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnimeFlix API", Version = "v1" });

    // Note: If you want to use the Authorization button in Swagger UI, you must add the JWT security definition here.
});

// Program.cs - Inside var builder.Services...

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllDev",
        builder =>
        {
            builder.AllowAnyOrigin() // Allows requests from any domain/port
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
// --- Build Application ---
var app = builder.Build();

// --- Middleware Configuration ---

// Enable Swagger UI and documentation during Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnimeFlix API v1");
    });
}

// Standard ASP.NET Core Middleware
app.UseHttpsRedirection();

// Note: Authentication middleware is typically required here for authorized endpoints (removed as requested)
app.UseAuthorization(); // This middleware is still required to enforce any [Authorize] attributes

// Maps controller endpoints to routing
app.MapControllers();

app.Run();