using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Aggregates
{
    public class Count : Aggregate
    {
        public Count(Field aggregate) : base(aggregate) { }
    }
}
