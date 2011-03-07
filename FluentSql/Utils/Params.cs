using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Collections;

namespace FluentSql.Utils
{
    internal class Params
    {
        public static IDictionary<string, object> ObjectToDicionary(object o)
        {
            var result = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            if (o == null)
            {
                return result;
            }
            if (o is Hashtable)
            {
                foreach (DictionaryEntry de in (Hashtable)o)
                {
                    result.Add(de.Key.ToString(), de.Value);
                }
            }
            else
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
