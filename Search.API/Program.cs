using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Search.API.Application;
using Search.API.Interfaces;
using Search.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// read file appsettings.json in Core Project
var coreProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Core");
var appsettingsPath = Path.Combine(coreProjectPath, "appsettings.json");
builder.Configuration.AddJsonFile(appsettingsPath, optional: true);

var connectionString = builder.Configuration.GetConnectionString("DbConnStr");

// Add DbContext Services
builder.Services.AddDbContext<SQLServerDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);

builder.Services.AddTransient<ISearchService, SearchService>();
builder.Services.AddTransient<ISearchRepository, SearchRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
