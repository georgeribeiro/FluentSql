using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    public class In : Expression
    {
        private Field _one;
        private string[] _sequence;
        
        public In(Field field, params string[] sequence)
        {
            _one = field;
            _sequence = (from s in sequence select String.Format("'{0}'", s)).ToArray();
        }

        public In(Field field, params object[] sequence)
        {
            _one = field;
            _sequence = (from s in sequence select s.ToString()).ToArray();
        }

        public override string ToSql()
        {
            return String.Format("{0} IN ({1})", FieldToString(_one), BuildSequence(_sequence));
        }

        private string BuildSequence(string[] sequence)
        {
                return string.Join(", ", sequence);
        }

        private string FieldToString(Field f)
        {
            return string.IsNullOrEmpty(f.Alias) ? f.Project : f.Alias;
        }
    }
}
