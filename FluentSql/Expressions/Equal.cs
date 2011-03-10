using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    public class Equal : Operator
    {
        public Equal(IStatement one, IStatement two) : base(one, two) { }

        public Equal(IStatement one, object two) : base(one, two) { }

        public override string ToSql()
        {
            if (Two == null)
            {
                return string.Format("{0} IS NULL", One);
            }
            return string.Format("{0} = {1}", One, Two);
        }
    }
}
