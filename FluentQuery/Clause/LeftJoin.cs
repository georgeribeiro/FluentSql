using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expressions;

namespace FluentQuery.Clause
{
    class LeftJoin : JoinBase
    {
        public LeftJoin(ITable table, IExpression expression) : base(table, expression) { }

        protected override string Clause
        {
            get
            {
                return "LEFT JOIN";
            }
        }
    }
}
