using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ListExtensions
{
    public static class EnumerableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable)
        {
            var dataTable = new DataTable();

            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            if (enumerable.GroupBy(i => i.GetType()).Count() > 1)
                throw new ArgumentException($"Can only table enumerables of the same type");

            var enumerableType = typeof(T);
            
            foreach (var field in enumerableType.GetFields())
            {
                dataTable.Columns.Add(new DataColumn()
                {
                    DataType = field.FieldType,
                    ColumnName = field.Name
                });
            }

            foreach (var item in enumerable)
            {
                var row = dataTable.NewRow();

                foreach (var field in enumerableType.GetFields())
                {
                    row[field.Name] = field.GetValue(item);
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
