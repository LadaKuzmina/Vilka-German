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

var connectionString = builder.Configuration.GetSection("Settings:DefaultConnection").Get<string>();
builder.Services.AddSingleton(new DbConfigurations() {ConnectString = connectionString});

var imageDirectory = builder.Configuration.GetSection("Settings:ImageDirectory").Get<string>();
builder.Services.AddSingleton(new DownloadConfigurations() {ImageDirectory = imageDirectory});

builder.Services.AddDbContext<GsDbContext>(options =>
{
    options.UseNpgsql(connectionString, sqlOptions => { sqlOptions.EnableRetryOnFailure(); });
});

builder.Services.AddTransient<IHeadersVisitor, HeadersVisitorDb>();
builder.Services.AddTransient<IProductVisitor, ProductsVisitorDb>();
builder.Services.AddTransient<IProjectVisitor, ProjectsVisitorDb>();
builder.Services.AddTransient<IPropertyVisitor, PropertiesVisitorDb>();
builder.Services.AddTransient<ISaleVisitor, SalesVisitorDb>();
builder.Services.AddTransient<IPhotoVisitor, PhotoVisitorDb>();

builder.Services.AddTransient<IHeadersChanger, HeadersChangerDb>();
builder.Services.AddTransient<IProjectChanger, ProjectChangerDb>();
builder.Services.AddTransient<IProductChanger, ProductChangerDb>();
builder.Services.AddTransient<ISaleChanger, SaleChangerDb>();
builder.Services.AddTransient<IPropertyChanger, PropertyChangerDb>();
builder.Services.AddTransient<IPhotoChanger, PhotoChangerDb>();

builder.Services.AddTransient<IHeadersService, HeadersService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IPropertyService, PropertyService>();
builder.Services.AddTransient<ISaleService, SaleService>();
builder.Services.AddTransient<IPhotoService, PhotoService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
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