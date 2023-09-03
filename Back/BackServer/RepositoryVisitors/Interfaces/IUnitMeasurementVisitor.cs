namespace BackServer.Repositories;

public interface IUnitMeasurementVisitor
{
    Task<IEnumerable<string>> GetAllAsync();
    Task<bool> ContainsAsync(string unitMeasurement);  
}