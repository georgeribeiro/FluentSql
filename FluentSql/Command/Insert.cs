using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FluentSql.Clause;
using FluentSql.Expressions;

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
            return string.Join(", ", (from f in FieldValues.Values select String.Format("@{0}", f)).ToArray());
        }
        #endregion

        #region ICommand Members
        public IList<Field> Projects
        {
            get
            {
                throw new NotSupportedException("Clause don't supported by command.");

            }
            set
            {
                throw new NotSupportedException("Clause don't supported by command.");

            }
        }
        public IList<IJoin> Joins
        {
            get
            {
                throw new NotSupportedException("Clause don't supported by command.");

            }
            set
            {
                throw new NotSupportedException("Clause don't supported by command.");

            }
        }
        public IList<IExpression> Wheres
        {
            get
            {
                throw new NotSupportedException("Clause don't supported by command.");

            }
            set
            {
                throw new NotSupportedException("Clause don't supported by command.");

            }
        }
        public IList<GroupBy> GroupBys
        {
            get
            {
                throw new NotSupportedException("Clause don't supported by command.");
            }
            set
            {
                throw new NotSupportedException("Clause don't supported by command.");

            }
        }
        public IDictionary<string, object> FieldValues { get; set; }
        public ITable Table { get; set; }
        public ICommand Values(object values)
        {
            IDictionary<string, object> keyvalue = Utils.Params.ObjectToDicionary(values);
            foreach (KeyValuePair<string, object> kvp in keyvalue)
            {
                FieldValues.Add(kvp.Key, Table.AddParam(string.Format("{0}_{1}", Table.Name, kvp.Key), kvp.Value));
            }
            return this;
        }
        
        public string ToSql()
        {
            return string.Format("INSERT INTO {0}({1}) VALUES({2})", Table.Name, BuildFields(), BuildValues());
        }

        public ICommand Project(params Field[] fields)
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

        public ICommand GroupBy(Field field)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        #endregion
    }
}
