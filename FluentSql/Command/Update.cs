using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Expressions;
using FluentSql.Clause;
using FluentSql.Aggregates;

namespace FluentSql.Command
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

        public ICommand Project(params IProjection[] projects)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public ICommand Project(IAggregate aggregate)
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
            Wheres.Add(expression);
            return this;
        }

        public ICommand Values(object values)
        {
            IDictionary<string, object> keyvalue = Utils.Params.ObjectToDicionary(values);
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

        public ICommand GroupBy(Field field)
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

        public IList<FluentSql.Expressions.IExpression> Wheres { get; set; }

        public IDictionary<string, object> FieldValues { get; set; }

        public ITable Table { get; set; }
        
        #endregion

        #region Build Members
        private string BuildValues()
        {
            return String.Join(", ", (from fv in FieldValues.Keys select String.Format("{0}={1}", fv, FieldValues[fv])).ToArray());    
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
