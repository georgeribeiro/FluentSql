using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FluentSql.Clause;
using FluentSql.Expressions;
using FluentSql.Aggregates;

namespace FluentSql.Command
{
    public class Insert : ICommand
    {
        public Insert(ITable table)
        {
            this.Table = table;
            this.FieldValues = new Dictionary<string, object>();
        }

        #region Build Members
        private string BuildFields()
        {
            return string.Join(", ", FieldValues.Keys.ToArray());
        }

        private string BuildValues()
        {
            return string.Join(", ", (from f in FieldValues.Values select String.Format("{0}", f)).ToArray());
        }
        #endregion

        #region ICommand Members
        public IDictionary<string, object> FieldValues { get; set; }
        public ITable Table { get; set; }
        public ICommand Values(object values)
        {
            IDictionary<string, object> keyvalue = Utils.ObjectToDicionary(values);
            foreach (KeyValuePair<string, object> kvp in keyvalue)
            {
                if (kvp.Value != null)
                {
                    FieldValues.Add(kvp.Key, String.Format("@{0}", Table.AddParam(kvp.Key, kvp.Value)));
                }
                else
                {
                    FieldValues.Add(kvp.Key, "NULL");
                }
            }
            return this;
        }
        
        public string ToSql()
        {
            return string.Format("INSERT INTO {0}({1}) VALUES({2})", Table.Name, BuildFields(), BuildValues());
        }

        public ICommand Project(params IProject[] projects)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public IJoin Join(ITable table)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public IJoin LeftJoin(ITable table)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public IJoin RightJoin(ITable table)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public IJoin InnerJoin(ITable table)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public ICommand Where(FluentSql.Expressions.IExpression expression)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public ICommand GroupBy(params IGroup[] groups)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public ICommand Having(IExpression expression)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public ICommand Count()
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public ICommand Top(int number)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        #endregion

        #region ICommand Members

        public ICommand OrderBy(params IOrder[] order)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        #endregion
    }
}
