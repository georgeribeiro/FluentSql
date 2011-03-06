using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expressions
{
    public class Expression : IExpression
    {
        #region IExpression Members

        public virtual string ToSql()
        {
            throw new NotImplementedException();
        }

        public Expression Or(Expression other)
        {
            return new LogicOperatorOr(this, other);
        }

        public Expression And(Expression other)
        {
            return new LogicOperatorAnd(this, other);
        }

        public static Expression operator |
            (Expression one, Expression two)
        {
            return one.Or(two);
        }

        public static Expression operator &
            (Expression one, Expression two)
        {
            return one.And(two);
        }

        public static Expression operator !
            (Expression expression)
        {
            return new Not(expression);
        }

        public override string ToString()
        {
            return ToSql();
        }

        #endregion
    }
}
