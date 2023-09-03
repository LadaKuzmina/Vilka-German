using System.Data;
using BackServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions;
using NpgsqlTypes;

namespace BackServer.Repositories;

public class UnitMeasurementVisitorDb : IUnitMeasurementVisitor
{
    private readonly GsDbContext _context;

    public UnitMeasurementVisitorDb(GsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<string>> GetAllAsync()
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        var sql = @"SELECT unit_measurement_value FROM units_measurement;";

        var unitMeasurements = new List<string>();
        await using var command = new NpgsqlCommand(sql, dbConnection);
        {
            await using var reader = command.ExecuteReader();
            while (reader.Read())
            {
               unitMeasurements.Add(reader.GetString(0));
            }
        }


        await dbConnection.CloseAsync();

        return unitMeasurements;
    }

    public async Task<bool> ContainsAsync(string unitMeasurement)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        var sql = @"SELECT unit_measurement_value FROM units_measurement WHERE unit_measurement_value = @UNIT;";

        var result = false;
        await using var command = new NpgsqlCommand(sql, dbConnection);
        {
            NpgsqlFunctions.AddParameters(command,
                new[]
                {
                    new NpgsqlParameter()
                        {ParameterName = "@UNIT", NpgsqlDbType = NpgsqlDbType.Text, Value = unitMeasurement}
                });
            await using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result = true;
            }
        }


        await dbConnection.CloseAsync();

        return result;
    }
}