using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery
{
    public interface IProjection
    {
        string ToSql();
        string Name { get; }
    }
}
