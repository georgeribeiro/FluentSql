using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expression;

namespace FluentQuery
{
    public struct Field
    {
        private Table _table;
        private string _name;

        public Field(Table table, string name)
        {
            this._table = table;
            this._name = name;
        }


        public string Alias
        {
            get
            {
                return String.Format("{0}_{1}", this._table.Name, this._name);
            }
        }

        public string Name
        {
            get
            {
                return String.Format("{0}.{1}", this._table.Name, this._name);
            }
        }

        #region IField Members

        public string ToSql()
        {
            if (_name == "*")
            {
                return String.Format("{0}", this.Name);
            }
            return String.Format("{0} AS {1}", this.Name, this.Alias);
        }

        #endregion

        public override string ToString()
        {
            return this.Name;
        }

        #region expressions

        public IExpression Equal(Field other)
        {
            return new Equal(this, other);
        }

        public INot Not
        {
            get
            {
                return new Not(this);
            }
        }

        #endregion

        #region override operators

        public static IExpression operator ==
            (Field one, Field two)
        {
            return one.Equal(two);
        }

        public static IExpression operator !=
            (Field one, Field two)
        {
            return one.Not.Equal(two);
        }

        #endregion
    }
}
