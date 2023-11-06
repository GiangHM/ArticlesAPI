using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayerShared
{
    internal static class CommandExtensions
    {
        public static void AddParameterWithValue(this IDbCommand command, string parameterName, object value)
        {
            var p = command.CreateParameter();
            p.ParameterName = parameterName;
            p.Value = value;
            command.Parameters.Add(p);
        }
    }
}
