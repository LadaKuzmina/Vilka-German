using Npgsql;
using NpgsqlTypes;

namespace NpgsqlDbExtensions;

public class NpgsqlFunctions
{
    public static void AddParameters(NpgsqlCommand command, IEnumerable<NpgsqlParameter> parameters)
    {
        foreach (var parameter in parameters)
        {
            command.Parameters.AddWithValue(parameter.ParameterName, parameter.NpgsqlDbType, parameter.Value);
        }
    }
    
    public static async Task<List<T>> TryGet<T>(string sql, NpgsqlConnection? connection,
        IEnumerable<NpgsqlParameter> parameters, Func<NpgsqlDataReader?, T> readerFunc)
    {
        var res = new List<T>();
        await using var command = new NpgsqlCommand(sql, connection);
        {
            AddParameters(command, parameters);
            
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                res.Add(readerFunc(reader));
            }
        }

        return res;
    }
    
    
    public static async Task Execute(string sql, NpgsqlConnection? connection,
        IEnumerable<NpgsqlParameter> parameters)
    {
        await using var command = new NpgsqlCommand(sql, connection);
        {
            AddParameters(command, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}