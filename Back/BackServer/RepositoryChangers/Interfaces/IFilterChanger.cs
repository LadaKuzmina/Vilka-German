using NpgsqlDbExtensions.Enums;

namespace BackServer.RepositoryChangers.Interfaces;

public interface IFilterChanger
{
    Task<bool> AddFilterHeadingOne(string headingTitle, string headingOneTitle);
    Task<bool> AddFilterHeadingTwo(string headingTwoTitle, string propertyTitle);
    
    Task<bool> DeleteHeadingOneFilter(string propertyTitle, string headingTitle);
    Task<bool> DeleteHeadingTwoFilter(string propertyTitle, string headingTitle);
    Task<bool> DeleteAllHeadingOneFilters(string headingOneTitle);
    Task<bool> DeleteAllHeadingTwoFilters(string headingTwoTitle);
    
    Task AddCountHeadingOneFilter(string headingTitle, string propertyValue, int count);
    Task AddCountHeadingTwoFilter(string headingTwoTitle, string propertyValue, int count);
}