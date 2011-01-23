using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expression;

namespace FluentQuery.Clause
{
    public class RightJoin : JoinBase
    {
        public RightJoin(Table table, ExpressionBase expression) : base(table, expression) { }

        protected override string Clause
        {
            get
            {
                return "RIGHT JOIN";
            }
        }
    }
}
