using BackServer.Repositories;
using BackServer.RepositoryChangers.Interfaces;

namespace BackServer.Services;

public class UnitMeasurementService : IUnitMeasurementService
{
    private readonly IUnitMeasurementChanger _changer;
    private readonly IUnitMeasurementVisitor _visitor;

    public UnitMeasurementService(IUnitMeasurementChanger changer, IUnitMeasurementVisitor visitor)
    {
        _changer = changer;
        _visitor = visitor;
    }

    public async Task<IEnumerable<string>> GetAllAsync()
    {
        return await _visitor.GetAllAsync();
    }

    public async Task AddAsync(string unitMeasurement)
    {
        var contains = await _visitor.ContainsAsync(unitMeasurement);
        if (!contains)
            await _changer.AddAsync(unitMeasurement);
    }

    public async Task DeleteAsync(string unitMeasurement)
    {
        await _changer.DeleteAsync(unitMeasurement);
    }
}