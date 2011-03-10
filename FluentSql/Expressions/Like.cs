using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    class Like : Operator
    {
        public Like(IStatement one, string two) : base(one, two) { }
        
        public override string ToSql()
        {
            return string.Format("{0} LIKE {1}", One, Two);
        }
    }
}
