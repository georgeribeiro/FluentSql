using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expression;

namespace FluentQuery
{
    public struct Field : IProjection
    {
        public Table _table;
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

        public Table Table
        {
            get
            {
                return _table;
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

        public ExpressionBase Equal(decimal obj)
        {
            return new Equal(this, obj);
        }

        public ExpressionBase Equal(DateTime obj)
        {
            return new Equal(this, obj);
        }

        public ExpressionBase Equal(bool obj)
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

        public ExpressionBase NotEqual(decimal obj)
        {
            return new NotEqual(this, obj);
        }

        public ExpressionBase NotEqual(DateTime obj)
        {
            return new NotEqual(this, obj);
        }

        public ExpressionBase NotEqual(bool obj)
        {
            return new Equal(this, obj);
        }

        public ExpressionBase LessThan(Field other)
        {
            return new LessThan(this, other);
        }

        public ExpressionBase LessThan(string other)
        {
            return new LessThan(this, other);
        }

        public ExpressionBase LessThan(int other)
        {
            return new LessThan(this, other);
        }

        public ExpressionBase LessThan(decimal other)
        {
            return new LessThan(this, other);
        }

        public ExpressionBase LessThan(DateTime other)
        {
            return new LessThan(this, other);
        }

        public ExpressionBase LessThanOrEqualTo(Field other)
        {
            return new LessThanOrEqualTo(this, other);
        }

        public ExpressionBase LessThanOrEqualTo(string other)
        {
            return new LessThanOrEqualTo(this, other);
        }

        public ExpressionBase LessThanOrEqualTo(int other)
        {
            return new LessThanOrEqualTo(this, other);
        }

        public ExpressionBase LessThanOrEqualTo(decimal other)
        {
            return new LessThanOrEqualTo(this, other);
        }

        public ExpressionBase LessThanOrEqualTo(DateTime other)
        {
            return new LessThanOrEqualTo(this, other);
        }

        public ExpressionBase GreaterThan(Field other)
        {
            return new GreaterThan(this, other);
        }

        public ExpressionBase GreaterThan(string other)
        {
            return new GreaterThan(this, other);
        }

        public ExpressionBase GreaterThan(int other)
        {
            return new GreaterThan(this, other);
        }

        public ExpressionBase GreaterThan(decimal other)
        {
            return new GreaterThan(this, other);
        }

        public ExpressionBase GreaterThan(DateTime other)
        {
            return new GreaterThan(this, other);
        }

        public ExpressionBase GreaterThanOrEqualTo(Field other)
        {
            return new GreaterThanOrEqualTo(this, other);
        }

        public ExpressionBase GreaterThanOrEqualTo(string other)
        {
            return new GreaterThanOrEqualTo(this, other);
        }

        public ExpressionBase GreaterThanOrEqualTo(int other)
        {
            return new GreaterThanOrEqualTo(this, other);
        }

        public ExpressionBase GreaterThanOrEqualTo(decimal other)
        {
            return new GreaterThanOrEqualTo(this, other);
        }

        public ExpressionBase GreaterThanOrEqualTo(DateTime other)
        {
            return new GreaterThanOrEqualTo(this, other);
        }

        public ExpressionBase Like(string expression_like)
        {
            return new Like(this, expression_like);
        }

        public ExpressionBase In(string[] sequence)
        {
            return new In(this, sequence);
        }

        public ExpressionBase In(int[] sequence)
        {
            return new In(this, sequence);
        }

        public ExpressionBase In(decimal[] sequence)
        {
            return new In(this, sequence);
        }

        public ExpressionBase In(DateTime[] sequence)
        {
            return new In(this, sequence);
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

        public static ExpressionBase operator ==
            (Field one, string two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator ==
            (Field one, int two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator ==
            (Field one, decimal two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator ==
            (Field one, DateTime two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator ==
            (Field one, bool two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator !=
            (Field one, Field two)
        {
            return one.NotEqual(two);
        }

        public static ExpressionBase operator !=
            (Field one, string two)
        {
            return one.NotEqual(two);
        }

        public static ExpressionBase operator !=
            (Field one, int two)
        {
            return one.NotEqual(two);
        }

        public static ExpressionBase operator !=
            (Field one, decimal two)
        {
            return one.NotEqual(two);
        }

        public static ExpressionBase operator !=
            (Field one, DateTime two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator !=
            (Field one, bool two)
        {
            return one.Equal(two);
        }

        public static ExpressionBase operator <
            (Field one, Field two)
        {
            return one.LessThan(two);
        }

        public static ExpressionBase operator <
            (Field one, string two)
        {
            return one.LessThan(two);
        }

        public static ExpressionBase operator <
            (Field one, int two)
        {
            return one.LessThan(two);
        }

        public static ExpressionBase operator <
            (Field one, decimal two)
        {
            return one.LessThan(two);
        }

        public static ExpressionBase operator <
            (Field one, DateTime two)
        {
            return one.LessThan(two);
        }

        public static ExpressionBase operator <=
            (Field one, Field two)
        {
            return one.LessThanOrEqualTo(two);
        }

        public static ExpressionBase operator <=
            (Field one, string two)
        {
            return one.LessThanOrEqualTo(two);
        }

        public static ExpressionBase operator <=
            (Field one, int two)
        {
            return one.LessThanOrEqualTo(two);
        }

        public static ExpressionBase operator <=
            (Field one, decimal two)
        {
            return one.LessThanOrEqualTo(two);
        }

        public static ExpressionBase operator <=
            (Field one, DateTime two)
        {
            return one.LessThanOrEqualTo(two);
        }

        public static ExpressionBase operator >
            (Field one, Field two)
        {
            return one.GreaterThan(two);
        }

        public static ExpressionBase operator >
            (Field one, string two)
        {
            return one.GreaterThan(two);
        }

        public static ExpressionBase operator >
            (Field one, int two)
        {
            return one.GreaterThan(two);
        }

        public static ExpressionBase operator >
            (Field one, decimal two)
        {
            return one.GreaterThan(two);
        }

        public static ExpressionBase operator >
            (Field one, DateTime two)
        {
            return one.GreaterThan(two);
        }

        public static ExpressionBase operator >=
            (Field one, Field two)
        {
            return one.GreaterThanOrEqualTo(two);
        }

        public static ExpressionBase operator >=
            (Field one, string two)
        {
            return one.GreaterThanOrEqualTo(two);
        }

        public static ExpressionBase operator >=
            (Field one, int two)
        {
            return one.GreaterThanOrEqualTo(two);
        }

        public static ExpressionBase operator >=
            (Field one, decimal two)
        {
            return one.GreaterThanOrEqualTo(two);
        }

        public static ExpressionBase operator >=
            (Field one, DateTime two)
        {
            return one.GreaterThanOrEqualTo(two);
        }

        #endregion
    }
}
