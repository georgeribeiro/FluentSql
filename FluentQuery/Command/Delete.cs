using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expressions;

namespace FluentQuery.Command
{
    public class Delete : ICommand
    {
        private IList<IExpression> Wheres = new List<IExpression>();
        public ITable Table { get; set; }

        public Delete(ITable table)
        {
            Table = table;
        }
        #region ICommand Members

        public string ToSql()
        {
            return String.Format("DELETE FROM {0}{1}", Table.Name, BuildWhere());
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
            Wheres.Add(expression);
            return this;
        }

        public ICommand GroupBy(Field field)
        {
            throw new NotImplementedException();
        }

        public ICommand Values(object values)
        {
            throw new NotImplementedException();
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
    }
}
