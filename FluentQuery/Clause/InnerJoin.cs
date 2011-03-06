using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expressions;

namespace FluentQuery.Clause
{
    public class InnerJoin : JoinBase
    {
        public InnerJoin(ITable table, IExpression expression) : base(table, expression) { }

        protected override string Clause
        {
            get
            {
                return "INNER JOIN";
            }
        }
    }
}
