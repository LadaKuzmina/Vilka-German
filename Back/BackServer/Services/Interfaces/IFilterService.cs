namespace BackServer.Services.Interfaces;

public interface IFilterService
{
    Task<IEnumerable<Entity.Property>> GetAllHeadingOneFilters(string headingOneTitle);
    Task<IEnumerable<Entity.Property>> GetAllHeadingTwoFilters(string headingTwoTitle);
    
    Task<bool> AddFilterHeadingOne( string headingOneTitle, string propertyTitle);
    Task<bool> AddFilterHeadingTwo(string headingTwoTitle, string propertyTitle);
    
    Task<bool> DeleteHeadingOneFilter(string propertyTitle, string headingOneTitle);
    Task<bool> DeleteHeadingTwoFilter(string propertyTitle, string headingTwoTitle);
    
    Task<bool> DeleteAllHeadingOneFilters(string headingOneTitle);
    Task<bool> DeleteAllHeadingTwoFilters(string headingTwoTitle);
}