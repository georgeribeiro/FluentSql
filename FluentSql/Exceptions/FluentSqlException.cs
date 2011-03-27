using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Exceptions
{
    public class FluentSqlException : Exception
    {
        public FluentSqlException(string message) : base(message) { }
    }
}
