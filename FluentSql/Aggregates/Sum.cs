using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Aggregates
{
    public class Sum: Aggregate
    {
        public Sum(Field aggregate) : base(aggregate) { }
    }
}
