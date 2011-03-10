using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Aggregates;

namespace FluentSql
{
    public static class Func
    {
        public static Aggregate Count(Field aggregate)
        {
            return new Count(aggregate);
        }

        public static Aggregate Sum(Field aggregate)
        {
            return new Sum(aggregate);
        }

        public static Aggregate Avg(Field aggregate)
        {
            return new Avg(aggregate);
        }

        public static Aggregate Min(Field aggregate)
        {
            return new Min(aggregate);
        }

        public static Aggregate Max(Field aggregate)
        {
            return new Max(aggregate);
        }
    }
}
