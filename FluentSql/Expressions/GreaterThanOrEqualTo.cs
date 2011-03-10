using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    class GreaterThanOrEqualTo : Operator
    {
        public GreaterThanOrEqualTo(IStatement one, IStatement two) : base(one, two) { }

        public GreaterThanOrEqualTo(IStatement one, object two) : base(one, two) { }

        public override string ToSql()
        {
            return string.Format("{0} >= {1}", One, Two);
        }
    }
}
