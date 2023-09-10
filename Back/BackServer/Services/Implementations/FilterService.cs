using BackServer.Repositories;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.Services.Interfaces;
using Entity;

namespace BackServer.Services;

public class FilterService : IFilterService
{
    private readonly IFilterVisitor _visitor;
    private readonly IFilterChanger _changer;

    public FilterService(IFilterVisitor visitor, IFilterChanger changer)
    {
        _visitor = visitor;
        _changer = changer;
    }

    public async Task<IEnumerable<Property>> GetAllHeadingOneFilters(string headingOneTitle)
    {
        return await _visitor.GetAllHeadingOneFilters(headingOneTitle);
    }

    public async Task<IEnumerable<Property>> GetAllHeadingTwoFilters(string headingTwoTitle)
    {
        return await _visitor.GetAllHeadingTwoFilters(headingTwoTitle);
    }


    public async Task<bool> AddFilterHeadingOne(string propertyTitle, string headingOneTitle)
    {
        return await _changer.AddFilterHeadingOne(propertyTitle, headingOneTitle);
    }

    public async Task<bool> AddFilterHeadingTwo(string headingTwoTitle, string propertyTitle)
    {
        return await _changer.AddFilterHeadingTwo(headingTwoTitle, propertyTitle);
    }


    public async Task<bool> DeleteHeadingOneFilter(string propertyTitle, string headingOneTitle)
    {
        return await _changer.DeleteHeadingOneFilter(propertyTitle, headingOneTitle);
    }

    public async Task<bool> DeleteHeadingTwoFilter(string propertyTitle, string headingTwoTitle)
    {
        return await _changer.DeleteHeadingTwoFilter(propertyTitle, headingTwoTitle);
    }


    public async Task<bool> DeleteAllHeadingOneFilters(string headingOneTitle)
    {
        return await _changer.DeleteAllHeadingOneFilters(headingOneTitle);
    }

    public async Task<bool> DeleteAllHeadingTwoFilters(string headingTwoTitle)
    {
        return await _changer.DeleteAllHeadingTwoFilters(headingTwoTitle);
    }
}