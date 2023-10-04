using BackServer.Services.Interfaces;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace BackServer.Controllers;

[ApiController]
[Route("[controller]")]
public class FiltersHandlersController : ControllerBase
{
    private readonly ILogger<PropertiesHandlersController> _logger;
    private readonly IFilterService _service;

    public FiltersHandlersController(ILogger<PropertiesHandlersController> logger, IFilterService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("~/GetPropertiesByHeadingOne")]
    public async Task<IEnumerable<Property>> GetPropertiesByHeadingOne(string headingOneTitle)
    {
        return await _service.GetAllHeadingOneFilters(headingOneTitle);
    }

    [HttpGet("~/GetPropertiesByHeadingTwo")]
    public async Task<IEnumerable<Property>> GetPropertiesByHeadingTwo(string headingTwoTitle)
    {
        return await _service.GetAllHeadingTwoFilters(headingTwoTitle);
    }

    [HttpPost("~/AddFilterHeadingOne")]
    public async Task<bool> AddFilterHeadingOne( string headingOneTitle, string propertyTitle)
    {
        return await _service.AddFilterHeadingOne(headingOneTitle, propertyTitle);
    }

    [HttpPost("~/AddFilterHeadingTwo")]
    public async Task<bool> AddFilterHeadingTwo(string headingTwoTitle, string propertyTitle)
    {
        return await _service.AddFilterHeadingTwo(headingTwoTitle, propertyTitle);
    }

    [HttpDelete("~/DeleteHeadingOneFilter")]
    public async Task<bool> DeleteHeadingOneFilter(string propertyTitle, string headingOneTitle)
    {
        return await _service.DeleteHeadingOneFilter(propertyTitle, headingOneTitle);
    }

    [HttpDelete("~/DeleteHeadingTwoFilter")]
    public async Task<bool> DeleteHeadingTwoFilter(string propertyTitle, string headingTwoTitle)
    {
        return await _service.DeleteHeadingTwoFilter(propertyTitle, headingTwoTitle);
    }

    [HttpDelete("~/DeleteAllHeadingOneFilters")]
    public async Task<bool> DeleteAllHeadingOneFilters(string headingOneTitle)
    {
        return await _service.DeleteAllHeadingOneFilters(headingOneTitle);
    }

    [HttpDelete("~/DeleteAllHeadingTwoFilters")]
    public async Task<bool> DeleteAllHeadingTwoFilters(string headingTwoTitle)
    {
        return await _service.DeleteAllHeadingTwoFilters(headingTwoTitle);
    }
}