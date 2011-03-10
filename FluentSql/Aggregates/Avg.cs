using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Aggregates
{
    public class Avg : Aggregate
    {
        public Avg(Field aggregate) : base(aggregate) { }
    }
}
