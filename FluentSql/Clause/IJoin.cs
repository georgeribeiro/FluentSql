using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Expressions;

namespace FluentSql.Clause
{
    public interface IJoin : IClause
    {
        ITable On(IExpression expression);    
    }
}
