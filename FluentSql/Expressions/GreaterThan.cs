using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    class GreaterThan : OperatorBase
    {
        public GreaterThan(Field one, Field two) : base(one, two) { }

        public GreaterThan(Field one, object two) : base(one, two) { }

        public override string ToSql()
        {
            return string.Format("{0} > {1}", One, Two);
        }
    }
}
