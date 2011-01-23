﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    class LogicOperatorAnd : ExpressionBase
    {
        private ExpressionBase _one;
        private ExpressionBase _two;

        public LogicOperatorAnd(ExpressionBase one, ExpressionBase two)
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