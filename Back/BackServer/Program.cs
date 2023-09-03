using BackServer;
using BackServer.Config;
using BackServer.Contexts;
using BackServer.Repositories;
using BackServer.RepositoryChangers.Implementations;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.Services;
using BackServer.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("Settings:ConnectionStrings:DefaultConnection").Get<string>();
builder.Services.AddSingleton(new DbConfigurations() {ConnectString = connectionString});

builder.Services.AddDbContext<GsDbContext>(options =>
{
    options.UseNpgsql(connectionString, sqlOptions => { sqlOptions.EnableRetryOnFailure(); });
});

ProjectBuilder.Build(builder);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(bldr =>
{
    bldr.AllowAnyOrigin();
    bldr.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();