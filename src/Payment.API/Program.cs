using Core;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Payment.API.Application;
using Payment.API.Interfaces;

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

// add DI
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddTransient<ISendMailService, SendMailService>();

// add EmailConfiguration
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
