using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Aggregates
{
    public interface IAggregate : IProjection
    {
        IAggregate As(string alias);
    }
}
