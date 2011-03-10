using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    class LessThanOrEqualTo : Operator
    {
        public LessThanOrEqualTo(IStatement one, IStatement two) : base(one, two) { }

        public LessThanOrEqualTo(IStatement one, object two) : base(one, two) { }

        public override string ToSql()
        {
            return string.Format("{0} <= {1}", One, Two);
        }
    }
}
