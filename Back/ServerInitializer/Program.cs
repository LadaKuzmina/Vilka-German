using BackServer;
using BackServer.Config;
using BackServer.Contexts;
using BackServer.Controllers;
using BackServer.Repositories;
using BackServer.RepositoryChangers.Implementations;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.Services;
using BackServer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServerInitializer.Config;
using ServerInitializer.Scripts;
using ServerInitializer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var imageDirectory = builder.Configuration.GetSection("Settings:ImageDirectory").Get<string>();
builder.Services.AddSingleton(new DownloadConfigurations() {ImageDirectory = imageDirectory});

var connectionString = builder.Configuration.GetSection("Settings:ConnectionStrings:DefaultConnection").Get<string>();
builder.Services.AddSingleton(new DbConfigurations() {ConnectString = connectionString});

builder.Services.AddDbContext<GsDbContext>(options =>
{
    options.UseNpgsql(connectionString, sqlOptions => { sqlOptions.EnableRetryOnFailure(); });
});

ProjectBuilder.Build(builder);

builder.Services.AddTransient<HeadingsHandlersController>();
builder.Services.AddTransient<ImageHandlersController>();
builder.Services.AddTransient<ProductsHandlersController>();
builder.Services.AddTransient<PropertiesHandlersController>();
builder.Services.AddTransient<UnitMeasurementController>();
builder.Services.AddTransient<FiltersHandlersController>();

builder.Services.AddSingleton<DbDataRequests>();
builder.Services.AddTransient<IDbInitializerService, DbInitializeService>();
builder.Services.AddTransient<IDbFillingService, DbFillingService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();