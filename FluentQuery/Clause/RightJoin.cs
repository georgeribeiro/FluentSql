using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expressions;

namespace FluentQuery.Clause
{
    public class RightJoin : JoinBase
    {
        public RightJoin(ITable table, IExpression expression) : base(table, expression) { }

        protected override string Clause
        {
            get
            {
                return "RIGHT JOIN";
            }
        }
    }
}
