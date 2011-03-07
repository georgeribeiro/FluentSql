using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Expressions;

namespace FluentSql.Clause
{
    public class Join : JoinBase
    {
        public Join(ITable table, ITable tableJoin) : base(table, tableJoin) { }

        protected override string Clause
        {
            get
            {
                return "JOIN";
            }
        }
    }
}
