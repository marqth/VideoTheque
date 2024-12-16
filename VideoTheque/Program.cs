using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;
using VideoTheque.Businesses.Genres;
using VideoTheque.Businesses.Supports;
using VideoTheque.Businesses.Personnes;
using VideoTheque.Businesses.Hosts;
using VideoTheque.Businesses.AgeRatings;
using VideoTheque.Businesses.Films;
using VideoTheque.Configurations;
using VideoTheque.Context;
using VideoTheque.Core;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.AgeRatings;
using VideoTheque.Repositories.Supports;
using VideoTheque.Repositories.Films;
using VideoTheque.Repositories.Personnes;
using VideoTheque.Repositories.Hosts;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Videotheque") ?? "Data Source=Videotheque.db";

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddSqlite<VideothequeDb>(connectionString);

builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<IGenresBusiness, GenresBusiness>();
builder.Services.AddScoped<ISupportsRepository, SupportsRepository>();
builder.Services.AddScoped<ISupportsBusiness, SupportsBusiness>();
builder.Services.AddScoped<IAgeRatingsRepository, AgeRatingsRepository>();
builder.Services.AddScoped<IAgeRatingsBusiness, AgeRatingsBusiness>();
builder.Services.AddScoped<IFilmsRepository, FilmsRepository>();
builder.Services.AddScoped<IFilmsBusiness, FilmsBusiness>();
builder.Services.AddScoped<IPersonnesRepository, PersonnesRepository>();
builder.Services.AddScoped<IPersonnesBusiness, PersonnesBusiness>();
builder.Services.AddScoped<IHostsRepository, HostsRepository>();
builder.Services.AddScoped<IHostsBusiness, HostsBusiness>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "VidéoThèque API",
        Description = "Gestion de sa collection de film.",
        Version = "v1"
    });
});

builder.Services.AddCors(option => option
    .AddDefaultPolicy(builder => builder
        .SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()));

var app = builder.Build();

// Create a scope to resolve the scoped services
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    // Register Mapster configurations
    MapsterConfig.RegisterMappings(serviceProvider);
}

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();