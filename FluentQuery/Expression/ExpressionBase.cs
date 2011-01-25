using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    public class ExpressionBase
    {
        #region IExpression Members

        public virtual string ToSql()
        {
            throw new NotImplementedException();
        }

        public ExpressionBase Or(ExpressionBase other)
        {
            return new LogicOperatorOr(this, other);
        }

        public ExpressionBase And(ExpressionBase other)
        {
            return new LogicOperatorAnd(this, other);
        }

        public static ExpressionBase operator |
            (ExpressionBase one, ExpressionBase two)
        {
            return one.Or(two);
        }

        public static ExpressionBase operator &
            (ExpressionBase one, ExpressionBase two)
        {
            return one.And(two);
        }

        public static ExpressionBase operator !
            (ExpressionBase obj)
        {
            return new Not(obj);
        }

        public override string ToString()
        {
            return ToSql();
        }

        #endregion
    }
}
