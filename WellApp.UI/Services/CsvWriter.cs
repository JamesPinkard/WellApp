using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WellApp.UI.Services
{
    public class CsvWriter
    {
        public static void ToCsv<T>(string path, string separator, IEnumerable<T> objectlist)
        {
            Type t = typeof(T);            
            PropertyInfo[] properties = t.GetProperties().ToArray();          
            

            string header = String.Join(separator, properties.Select(f => f.Name).ToArray());

            StringBuilder csvdata = new StringBuilder();
            csvdata.AppendLine(header);

            foreach (var o in objectlist)
                csvdata.AppendLine(ToCsvFields(separator, properties, o));

            System.IO.StreamWriter file = new System.IO.StreamWriter(path);
            file.WriteLineAsync(csvdata.ToString());            
        }

        public static string ToCsvFields(string separator, PropertyInfo[] properties, object o)
        {
            StringBuilder linie = new StringBuilder();

            foreach (var f in properties)
            {
                if (linie.Length > 0)
                    linie.Append(separator);

                var x = f.GetValue(o);

                if (x != null)
                    linie.Append(x.ToString());
            }

            return linie.ToString();
        }
    }
}
