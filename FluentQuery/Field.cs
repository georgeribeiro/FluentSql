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

        public override int GetHashCode()
        {
            return this.Project.GetHashCode();
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
                return String.Format("{0}.{1}", string.IsNullOrEmpty(_table.Alias) ? _table.Name : _table.Alias, Name);
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

        public ExpressionBase Equal(Field other)
        {
            return new Equal(this, other);
        }

        public ExpressionBase Equal(string obj)
        {
            return new Equal(this, obj);
        }

        public ExpressionBase Equal(int obj)
        {
            return new Equal(this, obj);
        }

        public ExpressionBase Equal(Decimal obj)
        {
            return new Equal(this, obj);
        }

        public ExpressionBase Equal(DateTime obj)
        {
            return new Equal(this, obj);
        }

        public ExpressionBase NotEqual(Field other)
        {
            return new NotEqual(this, other);
        }

        public ExpressionBase NotEqual(string obj)
        {
            return new NotEqual(this, obj);
        }

        public ExpressionBase NotEqual(int obj)
        {
            return new NotEqual(this, obj);
        }

        public ExpressionBase NotEqual(Decimal obj)
        {
            return new NotEqual(this, obj);
        }

        public ExpressionBase NotEqual(DateTime obj)
        {
            return new NotEqual(this, obj);
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

        public static ExpressionBase operator ==
            (Field one, Field two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator !=
            (Field one, Field two)
        {
            return one.NotEqual(two);
        }

        public static ExpressionBase operator ==
            (Field one, string two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator !=
            (Field one, string two)
        {
            return one.NotEqual(two);
        }

        public static ExpressionBase operator ==
            (Field one, int two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator !=
            (Field one, int two)
        {
            return one.NotEqual(two);
        }

        public static ExpressionBase operator ==
            (Field one, decimal two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator !=
            (Field one, decimal two)
        {
            return one.NotEqual(two);
        }

        public static ExpressionBase operator ==
            (Field one, DateTime two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator !=
            (Field one, DateTime two)
        {
            return one.Equal(two);
        }

        #endregion
    }
}
