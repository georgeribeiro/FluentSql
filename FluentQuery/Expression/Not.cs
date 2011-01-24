using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    public class Not : ExpressionBase
    {
        private ExpressionBase _expression;
        private Field _field;

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

        public ExpressionBase LessThan(Field other)
        {
            this._expression = _field.LessThan(other);
            return this;
        }

        public ExpressionBase LessThan(string other)
        {
            this._expression = _field.LessThan(other);
            return this;
        }

        public ExpressionBase LessThan(int other)
        {
            this._expression = _field.LessThan(other);
            return this;
        }

        public ExpressionBase LessThan(Decimal other)
        {
            this._expression = _field.LessThan(other);
            return this;
        }

        public ExpressionBase LessThan(DateTime other)
        {
            this._expression = _field.LessThan(other);
            return this;
        }

        public ExpressionBase LessThanOrEqualTo(Field other)
        {
            this._expression = _field.LessThanOrEqualTo(other);
            return this;
        }

        public ExpressionBase LessThanOrEqualTo(string other)
        {
            this._expression = _field.LessThanOrEqualTo(other);
            return this;
        }

        public ExpressionBase LessThanOrEqualTo(int other)
        {
            this._expression = _field.LessThanOrEqualTo(other);
            return this;
        }

        public ExpressionBase LessThanOrEqualTo(Decimal other)
        {
            this._expression = _field.LessThanOrEqualTo(other);
            return this;
        }

        public ExpressionBase LessThanOrEqualTo(DateTime other)
        {
            this._expression = _field.LessThanOrEqualTo(other);
            return this;
        }

        public ExpressionBase GreaterThan(Field other)
        {
            this._expression = _field.GreaterThan(other);
            return this;
        }

        public ExpressionBase GreaterThan(string other)
        {
            this._expression = _field.GreaterThan(other);
            return this;
        }

        public ExpressionBase GreaterThan(int other)
        {
            this._expression = _field.GreaterThan(other);
            return this;
        }

        public ExpressionBase GreaterThan(Decimal other)
        {
            this._expression = _field.GreaterThan(other);
            return this;
        }

        public ExpressionBase GreaterThan(DateTime other)
        {
            this._expression = _field.GreaterThan(other);
            return this;
        }

        public ExpressionBase GreaterThanOrEqualTo(Field other)
        {
            this._expression = _field.GreaterThanOrEqualTo(other);
            return this;
        }

        public ExpressionBase GreaterThanOrEqualTo(string other)
        {
            this._expression = _field.GreaterThanOrEqualTo(other);
            return this;
        }

        public ExpressionBase GreaterThanOrEqualTo(int other)
        {
            this._expression = _field.GreaterThanOrEqualTo(other);
            return this;
        }

        public ExpressionBase GreaterThanOrEqualTo(Decimal other)
        {
            this._expression = _field.GreaterThanOrEqualTo(other);
            return this;
        }

        public ExpressionBase GreaterThanOrEqualTo(DateTime other)
        {
            this._expression = _field.GreaterThanOrEqualTo(other);
            return this;
        }

        public ExpressionBase Like(string like_expression)
        {
            this._expression = _field.Like(like_expression);
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
