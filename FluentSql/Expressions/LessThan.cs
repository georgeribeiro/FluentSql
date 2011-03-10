using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    public class LessThan : Operator
    {
        public LessThan(IStatement one, IStatement two) : base(one, two) { }

        public LessThan(IStatement one, object two) : base(one, two) { }

        public override string ToSql()
        {
            return string.Format("{0} < {1}", One, Two);
        }
    }
}
