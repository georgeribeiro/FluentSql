using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Clause
{
    public class GroupBy : IClause
    {
        private Field _field;
        public GroupBy(Field field)
        {
            _field = field;
        }

        #region IClause Members

        public string ToSql()
        {
            return string.Format("{0}", this._field.ToSql());
        }

        #endregion
    }
}
