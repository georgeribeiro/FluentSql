using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Command
{
    public class Update : Select
    {
        private IDictionary<string, object> _fields_values = new Dictionary<string, object>();

        public Update(ITable table) : base(table) { }

        #region overrides members
        public override ICommand Project(params Field[] fields)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public override ICommand GroupBy(Field field)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public override ICommand InnerJoin(ITable table, FluentQuery.Expressions.IExpression expression)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public override ICommand Join(ITable table, FluentQuery.Expressions.IExpression expression)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public override ICommand LeftJoin(ITable table, FluentQuery.Expressions.IExpression expresssion)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public override ICommand RightJoin(ITable table, FluentQuery.Expressions.IExpression expression)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }
        #endregion

        public override ICommand Values(object values)
        {
            IDictionary<string, object> keyvalue = Utils.Params.ObjectToDicionary(values);
            foreach (KeyValuePair<string, object> kvp in keyvalue)
            {
                _fields_values.Add(kvp.Key, Table.AddParam(string.Format("{0}_{1}", Table.Name, kvp.Key), kvp.Value));
            }
            return this;
        }

        private string BuildValues()
        {
            return String.Join(", ", (from fv in _fields_values.Keys select String.Format("{0}=@{1}", fv, _fields_values[fv])).ToArray());    
        }

        public override string ToSql()
        {
            return String.Format("UPDATE {0} SET {1}{2}", this.Table.Name, BuildValues(), BuildWhere());
        }
    }
}
