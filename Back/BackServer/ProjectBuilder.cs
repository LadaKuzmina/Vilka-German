using BackServer.Repositories;
using BackServer.RepositoryChangers.Implementations;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.RepositoryVisitors;
using BackServer.RepositoryVisitors.Implementations;
using BackServer.Services;
using BackServer.Services.Interfaces;

namespace BackServer;

public class ProjectBuilder
{
    public static void Build(WebApplicationBuilder builder)
    {
        VisitorsBuild(builder);
        ChangerBuild(builder);
        ServiceBuild(builder);
    }

    public static void VisitorsBuild(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IHeadersVisitor, HeadersVisitorDb>();
        builder.Services.AddTransient<IProductVisitor, ProductsVisitorDb>();
        builder.Services.AddTransient<IProjectVisitor, ProjectsVisitorDb>();
        builder.Services.AddTransient<IPropertyVisitor, PropertiesVisitorDb>();
        builder.Services.AddTransient<ISaleVisitor, SalesVisitorDb>();
        builder.Services.AddTransient<IPhotoVisitor, PhotoVisitorDb>();
        builder.Services.AddTransient<IUnitMeasurementVisitor, UnitMeasurementVisitorDb>();
        builder.Services.AddTransient<IFilterVisitor, FilterVisitorDb>();
        builder.Services.AddTransient<DbEntityGetter>();
    }
    
    public static void ChangerBuild(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IHeadersChanger, HeadersChangerDb>();
        builder.Services.AddTransient<IProjectChanger, ProjectChangerDb>();
        builder.Services.AddTransient<IProductChanger, ProductChangerDb>();
        builder.Services.AddTransient<ISaleChanger, SaleChangerDb>();
        builder.Services.AddTransient<IPropertyChanger, PropertyChangerDb>();
        builder.Services.AddTransient<IPhotoChanger, PhotoChangerDb>();
        builder.Services.AddTransient<IUnitMeasurementChanger, UnitMeasurementChangerDb>();
        builder.Services.AddTransient<IFilterChanger, FilterChangerDb>();
    }

    public static void ServiceBuild(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IHeadersService, HeadersService>();
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddTransient<IProjectService, ProjectService>();
        builder.Services.AddTransient<IPropertyService, PropertyService>();
        builder.Services.AddTransient<ISaleService, SaleService>();
        builder.Services.AddTransient<IPhotoService, PhotoService>();
        builder.Services.AddTransient<IUnitMeasurementService, UnitMeasurementService>();
        builder.Services.AddTransient<IFilterService, FilterService>();
    }
}