using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expressions
{
    public class NotEqual : OperatorBase
    {
        public NotEqual(Field one, Field two) : base(one, two) { }

        public NotEqual(Field one, object two) : base(one, two) { }

        public override string ToSql()
        {
            return string.Format("{0} <> {1}", One, Two);
        }
    }
}
