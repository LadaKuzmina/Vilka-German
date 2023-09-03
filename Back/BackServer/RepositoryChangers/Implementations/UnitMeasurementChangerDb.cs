using System.Data;
using BackServer.Contexts;
using BackServer.RepositoryChangers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions;
using NpgsqlTypes;

namespace BackServer.RepositoryChangers.Implementations;

public class UnitMeasurementChangerDb : IUnitMeasurementChanger
{
    private readonly GsDbContext _context;

    public UnitMeasurementChangerDb(GsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(string unitMeasurement)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        var sql = @"INSERT INTO units_measurement (unit_measurement_value) VALUES (@UNIT);";

        await using var command = new NpgsqlCommand(sql, dbConnection);
        {
            NpgsqlFunctions.AddParameters(command,
                new[]
                {
                    new NpgsqlParameter()
                        {ParameterName = "@UNIT", NpgsqlDbType = NpgsqlDbType.Text, Value = unitMeasurement}
                });
            await command.ExecuteNonQueryAsync();
        }


        await dbConnection.CloseAsync();
    }
    
    public async Task DeleteAsync(string unitMeasurement)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        var sql = @"DELETE FROM units_measurement WHERE unit_measurement_value = @UNIT;";

        await using var command = new NpgsqlCommand(sql, dbConnection);
        {
            NpgsqlFunctions.AddParameters(command,
                new[]
                {
                    new NpgsqlParameter()
                        {ParameterName = "@UNIT", NpgsqlDbType = NpgsqlDbType.Text, Value = unitMeasurement}
                });
            await command.ExecuteNonQueryAsync();
        }


        await dbConnection.CloseAsync();
    }
}