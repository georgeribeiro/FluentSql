using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    class Equal : IEqual
    {
        private Field _one;
        private Field _two;
        public Equal(Field one, Field two)
        {
            _one = one;
            _two = two;
        }

        #region IExpression Members

        public string ToSql()
        {
            return String.Format("{0} = {1}", _one.Name, _two.Name);
        }

        #endregion
    }
}
