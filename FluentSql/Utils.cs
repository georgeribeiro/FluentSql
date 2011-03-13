using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Collections;

namespace FluentSql
{
    public static class Utils
    {
        internal static IDictionary<string, object> ObjectToDicionary(object o)
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

        public static void AddFrom(this Hashtable self, Hashtable other)
        {
            foreach (DictionaryEntry de in other)
            {
                self.Add(de.Key, de.Value);
            }
        }

        public const object NULL = null;
    }
}
