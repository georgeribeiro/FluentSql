using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    public class Equal : OperatorBase
    {
        public Equal(Field one, Field two) : base(one, two) { }

        public Equal(Field one, string two) : base(one, two) { }

        public Equal(Field one, int two) : base(one, two) { }

        public Equal(Field one, decimal two) : base(one, two) { }

        public Equal(Field one, DateTime two) : base(one, two) { }

        public override string ToSql()
        {
            return string.Format("{0} = {1}", One, Two);
        }
    }
}
