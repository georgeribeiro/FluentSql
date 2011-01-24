using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    class Like : OperatorBase
    {
        public Like(Field one, string two) : base(one, two) { }
        
        public override string ToSql()
        {
            return string.Format("{0} LIKE {1}", One, Two);
        }
    }
}
