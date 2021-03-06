﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Expressions;

namespace FluentSql.Clause
{
    public class RightJoin : JoinBase
    {
        public RightJoin(ITable table, ITable tableJoin) : base(table, tableJoin) { }

        protected override string Clause
        {
            get
            {
                return "RIGHT JOIN";
            }
        }
    }
}
