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
            string alias_one = _one.Alias != null ? _one.Alias : _one.Project;
            string alias_two = _two.Alias != null ? _two.Alias : _two.Project;
            return String.Format("{0} = {1}", alias_one, alias_two);
        }

        #endregion
    }
}
