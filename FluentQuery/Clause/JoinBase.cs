using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expression;

namespace FluentQuery.Clause
{
    public class JoinBase : IJoin
    {
        protected Table Table { get; set; }
        protected ExpressionBase Expression { get; set; }
        protected virtual string Clause
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public JoinBase(Table table, ExpressionBase expression)
        {
            Table = table;
            Expression = expression;
        }

        #region IClause Members

        public virtual string ToSql()
        {
            if (!string.IsNullOrEmpty(Table.Alias))
            {
                return string.Format("{0} {1} AS {2} ON {3}", Clause, Table.Name, Table.Alias, Expression.ToSql());
            }
            return string.Format("{0} {1} ON {2}", Clause, Table.Name, Expression.ToSql());
        }

        #endregion
    }
}
