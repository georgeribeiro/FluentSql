using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expression;

namespace FluentQuery
{
    public struct Field : IProjection
    {
        private Table _table;
        private string _name;
        private string _alias;

        public Field(Table table, string name)
        {
            _table = table;
            _name = name;
            _alias = null;
        }

        public Field As(string alias)
        {
            _alias = alias;
            return this;
        }

        public string Alias
        {
            get
            {
                return _alias;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Project
        {
            get
            {
                return String.Format("{0}.{1}", _table.Alias, _name);
            }
        }

        #region IProjection Members

        public string ToSql()
        {
            if (!string.IsNullOrEmpty(this.Alias))
            {
                return string.Format("{0} AS {1}", this.Project, this.Alias);
            }
            else
            {
                return string.Format("{0}", this.Project);
            }
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

        public Not Not
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
