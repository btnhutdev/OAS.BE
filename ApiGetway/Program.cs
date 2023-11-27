using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseOcelot().Wait();

app.Run();