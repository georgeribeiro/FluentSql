using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    public interface INot : IExpression
    {
        IExpression Equal(Field field);
    }
}
