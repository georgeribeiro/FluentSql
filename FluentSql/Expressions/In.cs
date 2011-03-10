using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    public class In : Expression
    {
        private IStatement _one;
        private string[] _sequence;
        private ITable _table;

        public In(IStatement field, params string[] sequence)
        {
            _one = field;
            _sequence = (from s in sequence select String.Format("'{0}'", s)).ToArray();
        }

        public In(IStatement field, params object[] sequence)
        {
            _one = field;
            _sequence = (from s in sequence select s.ToString()).ToArray();
        }

        public In(IStatement field, ITable table)
        {
            _one = field;
            _table = table;
        }

        public override string ToSql()
        {
            if (_table == null)
            {
                return String.Format("{0} IN ({1})", StatementToString(_one), BuildSequence(_sequence));
            }
            else
            {
                return string.Format("{0} IN ({1})", StatementToString(_one), _table.ToSql());
            }
        }

        private string BuildSequence(string[] sequence)
        {
                return string.Join(", ", sequence);
        }

        private string StatementToString(IStatement s)
        {
            return string.IsNullOrEmpty(s.Alias) ? s.Project : s.Alias;
        }
    }
}
