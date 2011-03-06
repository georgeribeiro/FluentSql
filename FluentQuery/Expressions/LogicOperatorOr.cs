using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expressions
{
    public class LogicOperatorOr : Expression
    {
        private IExpression _one;
        private IExpression _two;

        public LogicOperatorOr(IExpression one, IExpression two)
        {
            _one = one;
            _two = two;
        }

        #region IExpression Members

        public override string ToSql()
        {
            return string.Format("({0}) OR ({1})", _one.ToSql(), _two.ToSql());
        }

        #endregion
    }
}
