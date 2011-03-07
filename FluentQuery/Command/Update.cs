using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expressions;
using FluentQuery.Clause;

namespace FluentQuery.Command
{
    internal class Update : ICommand
    {
        public Update(ITable table, IList<IExpression> wheres)
        {
            this.Table = table;
            this.Wheres = wheres;
            this.FieldValues = new Dictionary<string, object>();
        }

        #region ICommand Members

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

        public ICommand Where(FluentQuery.Expressions.IExpression expression)
        {
            Wheres.Add(expression);
            return this;
        }

        public ICommand Values(object values)
        {
            IDictionary<string, object> keyvalue = Utils.Params.ObjectToDicionary(values);
            foreach (KeyValuePair<string, object> kvp in keyvalue)
            {
                FieldValues.Add(kvp.Key, Table.AddParam(string.Format("{0}_{1}", Table.Name, kvp.Key), kvp.Value));
            }
            return this;
        }

        public ICommand GroupBy(Field field)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

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

        public IList<FluentQuery.Clause.IJoin> Joins
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

        public IList<FluentQuery.Expressions.IExpression> Wheres { get; set; }

        public IList<FluentQuery.Clause.GroupBy> GroupBys
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
        
        #endregion

        #region Build Members
        private string BuildValues()
        {
            return String.Join(", ", (from fv in FieldValues.Keys select String.Format("{0}=@{1}", fv, FieldValues[fv])).ToArray());    
        }

        private string BuildWhere()
        {
            if (Wheres.Count > 0)
            {
                return " WHERE " + string.Join(" AND ", (from e in Wheres select e.ToSql()).ToArray());
            }
            return string.Empty;
        }
        #endregion

        public string ToSql()
        {
            return String.Format("UPDATE {0} SET {1}{2}", this.Table.Name, BuildValues(), BuildWhere());
        }
    }
}
