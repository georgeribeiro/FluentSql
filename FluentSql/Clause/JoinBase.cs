using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Expressions;

namespace FluentSql.Clause
{
    public class JoinBase : IJoin
    {
        protected ITable Table { get; set; }
        private ITable TableJoin { get; set; }
        protected IExpression Expression { get; set; }
        protected virtual string Clause
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public JoinBase(ITable table, ITable tableJoin)
        {
            Table = table;
            TableJoin = tableJoin;
        }

        public ITable On(IExpression expression)
        {
            Expression = expression;
            return Table;
        }


        #region IClause Members

        public virtual string ToSql()
        {
            if (!string.IsNullOrEmpty(TableJoin.Alias))
            {
                return string.Format("{0} {1} AS {2} ON {3}", Clause, TableJoin.Name, TableJoin.Alias, Expression.ToSql());
            }
            return string.Format("{0} {1} ON {2}", Clause, TableJoin.Name, Expression.ToSql());
        }

        #endregion
    }
}
