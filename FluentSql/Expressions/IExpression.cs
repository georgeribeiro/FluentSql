using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    public interface IExpression
    {
        string ToSql();
        Expression Or(Expression other);
        Expression And(Expression other);
    }
}
