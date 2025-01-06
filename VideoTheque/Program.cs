using System.Text.Json.Serialization;
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
using VideoTheque.Repositories.AgeRatings;
using VideoTheque.Repositories.Films;


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
builder.Services.AddScoped<IFilmsRepository, FilmsRepository>();
builder.Services.AddScoped<IFilmsBusiness, FilmsBusiness>();
builder.Services.AddScoped<IHostsRepository, HostsRepository>();
builder.Services.AddScoped<IHostsBusiness, HostsBusiness>();
builder.Services.AddScoped<IFilmsRepository, FilmsRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
    });

builder.Services.AddCors(option => option
    .AddDefaultPolicy(builder => builder
        .SetIsOriginAllowed(_=> true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VidéoThèque API V1");
    });
}

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();