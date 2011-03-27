using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Exceptions
{
    public class InvalidClauseException : FluentSqlException
    {
        public InvalidClauseException(string message) : base(message) { }
    }
}
