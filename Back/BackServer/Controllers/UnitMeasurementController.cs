using BackServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackServer.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitMeasurementController: ControllerBase
{
    private readonly ILogger<PropertiesHandlersController> _logger;
    private readonly IUnitMeasurementService _service;

    public UnitMeasurementController(ILogger<PropertiesHandlersController> logger, IUnitMeasurementService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("~/GetAllUnitMeasurements")]
    public async Task<ObjectResult> GetAllUnitMeasurements()
    {
        try
        {
            return StatusCode(200, await _service.GetAllAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost("~/AddUnitsMeasurement")]
    public async Task<ObjectResult> AddUnitsMeasurement(IEnumerable<string> unitsMeasurement)
    {
        foreach (var unit in unitsMeasurement)
        {
            try
            {
                await _service.AddAsync(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(400, $"Не удалось добавить такую единицу измерения: {unit}.\n {e.Message}");
            }
        }

        return StatusCode(200, "");
    }

    [HttpPost("~/AddUnitMeasurement")]
    public async Task<ObjectResult> AddUnitMeasurement(string unitMeasurement)
    {
        try
        {
            await _service.AddAsync(unitMeasurement);
            return StatusCode(200, "");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(400, $"Не удалось добавить такую единицу измерения: {unitMeasurement}.\n {e.Message}");
        }
    }

    [HttpPost("~/DeleteUnitMeasurement")]
    public async Task<ObjectResult> DeleteUnitMeasurement(string unitMeasurement)
    {
        try
        {
            await _service.DeleteAsync(unitMeasurement);
            return StatusCode(200, "");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(400, $"Не удалось удалить такую единицу измерения: {unitMeasurement}.\n {e.Message}");
        }
    }
}