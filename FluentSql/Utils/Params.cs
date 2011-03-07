using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace FluentSql.Utils
{
    internal class Params
    {
        public static IDictionary<string, object> ObjectToDicionary(object o)
        {
            var result = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            if (o != null)
            {
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(o))
                {
                    result.Add(prop.Name, prop.GetValue(o));
                }    
            }
            
            return result;
        }
    }
}
