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
}