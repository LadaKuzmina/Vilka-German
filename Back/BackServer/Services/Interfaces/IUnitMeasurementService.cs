namespace BackServer.Services;

public interface IUnitMeasurementService
{
    Task<IEnumerable<string>> GetAllAsync();
    Task AddAsync(string unitMeasurement);
    Task DeleteAsync(string unitMeasurement);
}