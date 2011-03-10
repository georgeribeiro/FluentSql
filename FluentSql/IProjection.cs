using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql
{
    public interface IProjection
    {
        string ToSql();
    }
}
