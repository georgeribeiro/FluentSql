using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    public class NotEqual : OperatorBase
    {
        public NotEqual(Field one, Field two) : base(one, two) { }

        public NotEqual(Field one, string two) : base(one, two) { }

        public NotEqual(Field one, int two) : base(one, two) { }

        public NotEqual(Field one, decimal two) : base(one, two) { }

        public NotEqual(Field one, DateTime two) : base(one, two) { }

        public override string ToSql()
        {
            return string.Format("{0} <> {1}", One, Two);
        }
    }
}
