using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    public class Not : Expression
    {
        private IExpression _expression;
        private IStatement _statement;

        public Not(IStatement statement)
        {
            _statement = statement;
            _expression = null;
        }

        public Not(IExpression expression)
        {
            _expression = expression;
        }

        public IExpression Equal(IStatement other)
        {
            this._expression = _statement.Equal(other);
            return this;
        }

        public IExpression Equal(object other)
        {
            this._expression = _statement.Equal(other);
            return this;
        }

        public IExpression LessThan(IStatement other)
        {
            this._expression = _statement.LessThan(other);
            return this;
        }

        public IExpression LessThan(object other)
        {
            this._expression = _statement.LessThan(other);
            return this;
        }

        public IExpression LessThanOrEqualTo(IStatement other)
        {
            this._expression = _statement.LessThanOrEqualTo(other);
            return this;
        }

        public IExpression LessThanOrEqualTo(object other)
        {
            this._expression = _statement.LessThanOrEqualTo(other);
            return this;
        }

        public IExpression GreaterThan(IStatement other)
        {
            this._expression = _statement.GreaterThan(other);
            return this;
        }

        public IExpression GreaterThan(object other)
        {
            this._expression = _statement.GreaterThan(other);
            return this;
        }

        public IExpression GreaterThanOrEqualTo(IStatement other)
        {
            this._expression = _statement.GreaterThanOrEqualTo(other);
            return this;
        }

        public IExpression GreaterThanOrEqualTo(object other)
        {
            this._expression = _statement.GreaterThanOrEqualTo(other);
            return this;
        }

        public IExpression Like(string like_expression)
        {
            this._expression = _statement.Like(like_expression);
            return this;
        }

        public IExpression In(params string[] sequence)
        {
            this._expression = _statement.In(sequence);
            return this;
        }

        public IExpression In(params object[] sequence)
        {
            this._expression = _statement.In(sequence);
            return this;
        }

        public IExpression In(ITable table)
        {
            this._expression = _statement.In(table);
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
