using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    public class Not : INot
    {
        IExpression _expression;
        Field _field;

        public Not(Field field)
        {
            _field = field;
        }

        public IExpression Equal(Field other)
        {
            this._expression = _field.Equal(other);
            return this;
        }

        #region IExpression Members

        public string ToSql()
        {
            return String.Format("NOT {0}", _expression.ToSql());
        }

        #endregion
    }
}
