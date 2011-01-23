using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    public class Not : ExpressionBase
    {
        ExpressionBase _expression;
        Field _field;

        public Not(Field field)
        {
            _field = field;
            _expression = null;
        }

        public ExpressionBase Equal(Field other)
        {
            this._expression = _field.Equal(other);
            return this;
        }

        public ExpressionBase Equal(string other)
        {
            this._expression = _field.Equal(other);
            return this;
        }

        public ExpressionBase Equal(int other)
        {
            this._expression = _field.Equal(other);
            return this;
        }

        public ExpressionBase Equal(Decimal other)
        {
            this._expression = _field.Equal(other);
            return this;
        }

        public ExpressionBase Equal(DateTime other)
        {
            this._expression = _field.Equal(other);
            return this;
        }

        #region ExpressionBase Members

        public override string ToSql()
        {
            return String.Format("NOT {0}", _expression.ToSql());
        }

        #endregion
    }
}
