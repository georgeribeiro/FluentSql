using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    class LogicOperatorAnd : Expression
    {
        private Expression _one;
        private Expression _two;

        public LogicOperatorAnd(Expression one, Expression two)
        {
            _one = one;
            _two = two;
        }

        #region IExpression Members

        public override string ToSql()
        {
            return string.Format("({0}) AND ({1})", _one.ToSql(), _two.ToSql());
        }

        #endregion
    }
}
