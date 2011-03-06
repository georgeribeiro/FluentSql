using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FluentQuery.Command
{
    public class Insert : ICommand
    {
        public ITable _table;

        private IDictionary<string, object> _fields_values = new Dictionary<string, object>();
        public Insert(ITable table)
        {
            _table = table;
        }

        #region Build Members
        private string BuildFields()
        {
            return string.Join(", ", _fields_values.Keys.ToArray());
        }

        private string BuildValues()
        {
            return string.Join(", ", (from f in _fields_values.Values select String.Format("@{0}", f)).ToArray());
        }
        #endregion

        #region ICommand Members

        public ICommand Values(object values)
        {
            IDictionary<string, object> keyvalue = Utils.Params.ObjectToDicionary(values);
            foreach (KeyValuePair<string, object> kvp in keyvalue)
            {
                _fields_values.Add(kvp.Key, _table.AddParam(string.Format("{0}_{1}", _table.Name, kvp.Key), kvp.Value));
            }
            return this;
        }
        
        public string ToSql()
        {
            return string.Format("INSERT INTO {0}({1}) VALUES({2})", _table.Name, BuildFields(), BuildValues());
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public ICommand Project(params Field[] fields)
        {
            throw new NotImplementedException();
        }

        public ICommand Join(ITable table, FluentQuery.Expressions.IExpression expression)
        {
            throw new NotImplementedException();
        }

        public ICommand LeftJoin(ITable table, FluentQuery.Expressions.IExpression expression)
        {
            throw new NotImplementedException();
        }

        public ICommand RightJoin(ITable table, FluentQuery.Expressions.IExpression expression)
        {
            throw new NotImplementedException();
        }

        public ICommand InnerJoin(ITable table, FluentQuery.Expressions.IExpression expression)
        {
            throw new NotImplementedException();
        }

        public ICommand Where(FluentQuery.Expressions.IExpression expression)
        {
            throw new NotImplementedException();
        }

        public ICommand GroupBy(Field field)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
