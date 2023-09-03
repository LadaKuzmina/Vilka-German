namespace BackServer.RepositoryChangers.Interfaces;

public interface IUnitMeasurementChanger
{
    Task AddAsync(string unitMeasurement);
    Task DeleteAsync(string unitMeasurement);
}