using Amazon.S3;
using Core;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Product.API.Utilities;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// read file appsettings.json in Core Project
var coreProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Core");
var appsettingsPath = Path.Combine(coreProjectPath, "appsettings.json");
builder.Configuration.AddJsonFile(appsettingsPath, optional: true);

var connectionString = builder.Configuration.GetConnectionString("DbConnStr");

// Add DbContext Services
builder.Services.AddDbContext<SQLServerDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient).AddDependencies();

// add Swagger Services
builder.Services.AddSwaggerGen();

// add aws s3 service
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

// add email service
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

// add Hangfire Services
builder.Services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
       {
           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
           QueuePollInterval = TimeSpan.Zero,
           UseRecommendedIsolationLevel = true,
           DisableGlobalLocks = true
       }));
builder.Services.AddHangfireServer();

// add Redis & SignalR service
builder.Services.AddSignalR();
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseHangfireDashboard("/dashboard");

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

app.Run();