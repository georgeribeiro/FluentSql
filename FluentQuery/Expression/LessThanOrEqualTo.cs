using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    class LessThanOrEqualTo : OperatorBase
    {
        public LessThanOrEqualTo(Field one, Field two) : base(one, two) { }

        public LessThanOrEqualTo(Field one, string two) : base(one, two) { }

        public LessThanOrEqualTo(Field one, int two) : base(one, two) { }

        public LessThanOrEqualTo(Field one, decimal two) : base(one, two) { }

        public LessThanOrEqualTo(Field one, DateTime two) : base(one, two) { }

        public override string ToSql()
        {
            return string.Format("{0} <= {1}", One, Two);
        }
    }
}
