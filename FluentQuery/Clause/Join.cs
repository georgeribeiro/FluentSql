using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expression;

namespace FluentQuery.Clause
{
    public class Join : IJoin
    {
        private Table _table;
        private IExpression _expression;

        public Join(Table table, IExpression expression)
        {
            _table = table;
            _expression = expression;
        }

        #region IClause Members

        public string ToSql()
        {
            return String.Format("JOIN {0} ON {1}", _table.Name, _expression.ToSql());
        }

        #endregion
    }
}
