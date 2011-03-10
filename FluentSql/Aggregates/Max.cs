using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Aggregates
{
    class Max : Aggregate
    {
        public Max(Field aggregate) : base(aggregate) { }
    }
}
