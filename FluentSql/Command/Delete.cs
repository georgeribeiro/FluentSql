using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Expressions;
using FluentSql.Clause;
using FluentSql.Aggregates;

namespace FluentSql.Command
{
    public class Delete : ICommand
    {
        public Delete(ITable table, IList<IExpression> wheres)
        {
            this.Table = table;
            this.Wheres = wheres;
        }
        #region ICommand Members
        public IList<IExpression> Wheres { get; set; }
        public IDictionary<string, object> FieldValues { get; set; }
        public ITable Table { get; set; }

        public string ToSql()
        {
            return String.Format("DELETE FROM {0}{1}", Table.Name, BuildWhere());
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
            Wheres.Add(expression);
            return this;
        }

        public ICommand GroupBy(params IGroup[] groups)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public ICommand Having(IExpression expression)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }

        public ICommand Values(object values)
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

        #region Build Members
        protected string BuildWhere()
        {
            if (Wheres.Count > 0)
            {
                return " WHERE " + string.Join(" AND ", (from e in Wheres select e.ToSql()).ToArray());
            }
            return string.Empty;
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
