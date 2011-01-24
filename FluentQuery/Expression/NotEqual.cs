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

        public NotEqual(Field one, bool two)
        {
            One = FieldToString(one);
            string param = one._table.Param(String.Format("{0}_{1}", one.Table.Name, one.Name), two);
            Two = "@" + param;
        }

        public override string ToSql()
        {
            return string.Format("{0} <> {1}", One, Two);
        }
    }
}
