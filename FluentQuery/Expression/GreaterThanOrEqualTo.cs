using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    class GreaterThanOrEqualTo : OperatorBase
    {
        public GreaterThanOrEqualTo(Field one, Field two) : base(one, two) { }

        public GreaterThanOrEqualTo(Field one, string two) : base(one, two) { }

        public GreaterThanOrEqualTo(Field one, int two) : base(one, two) { }

        public GreaterThanOrEqualTo(Field one, decimal two) : base(one, two) { }

        public GreaterThanOrEqualTo(Field one, DateTime two) : base(one, two) { }

        public override string ToSql()
        {
            return string.Format("{0} >= {1}", One, Two);
        }
    }
}
