using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Aggregates
{
    class Min : Aggregate
    {
        public Min(Field aggregate) : base(aggregate) { }
    }
}
