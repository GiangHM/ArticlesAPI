using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayerShared
{
    public static class GetDBValueExtensions
    {
        public static T GetDBValue<T>(this IDataReader reader, string columnName)
        {
            var value = reader[columnName]; // read column value

            return value == DBNull.Value ? default(T) : (T)value;
        }
        public static T GetDBValue<T>(this DataRow row, string columnName)
        {
            var value = row[columnName]; // read column value

            return value == DBNull.Value ? default(T) : (T)value;
        }
    }
}
