using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    public class Not : Expression
    {
        private IExpression _expression;
        private Field _field;

        public Not(Field field)
        {
            _field = field;
            _expression = null;
        }

        public Not(IExpression expression)
        {
            _expression = expression;
        }

        public IExpression Equal(Field other)
        {
            this._expression = _field.Equal(other);
            return this;
        }

        public IExpression Equal(object other)
        {
            this._expression = _field.Equal(other);
            return this;
        }

        public IExpression LessThan(Field other)
        {
            this._expression = _field.LessThan(other);
            return this;
        }

        public IExpression LessThan(object other)
        {
            this._expression = _field.LessThan(other);
            return this;
        }

        public IExpression LessThanOrEqualTo(Field other)
        {
            this._expression = _field.LessThanOrEqualTo(other);
            return this;
        }

        public IExpression LessThanOrEqualTo(object other)
        {
            this._expression = _field.LessThanOrEqualTo(other);
            return this;
        }

        public IExpression GreaterThan(Field other)
        {
            this._expression = _field.GreaterThan(other);
            return this;
        }

        public IExpression GreaterThan(object other)
        {
            this._expression = _field.GreaterThan(other);
            return this;
        }

        public IExpression GreaterThanOrEqualTo(Field other)
        {
            this._expression = _field.GreaterThanOrEqualTo(other);
            return this;
        }

        public IExpression GreaterThanOrEqualTo(object other)
        {
            this._expression = _field.GreaterThanOrEqualTo(other);
            return this;
        }

        public IExpression Like(string like_expression)
        {
            this._expression = _field.Like(like_expression);
            return this;
        }

        public IExpression In(params string[] sequence)
        {
            this._expression = _field.In(sequence);
            return this;
        }

        public IExpression In(params object[] sequence)
        {
            this._expression = _field.In(sequence);
            return this;
        }

        #region IExpression Members

        public override string ToSql()
        {
            return String.Format("NOT {0}", _expression.ToSql());
        }

        #endregion
    }
}
