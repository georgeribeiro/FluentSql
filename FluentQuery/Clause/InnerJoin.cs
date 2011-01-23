using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expression;

namespace FluentQuery.Clause
{
    public class InnerJoin : JoinBase
    {
        public InnerJoin(Table table, ExpressionBase expression) : base(table, expression) { }

        protected override string Clause
        {
            get
            {
                return "INNER JOIN";
            }
        }
    }
}
