using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expressions;

namespace FluentQuery
{
    public class Field
    {
        public Field(ITable table, string name)
        {
            Table = table;
            Name = name;
            Alias = null;
        }

        public override int GetHashCode()
        {
            return this.Project.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
        }

        public Field As(string alias)
        {
            Alias = alias;
            return this;
        }

        public string Alias
        {
            get; 
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public ITable Table
        {
            get;
            private set;
        }

        public string Project
        {
            get
            {
                return String.Format("{0}.{1}", string.IsNullOrEmpty(Table.Alias) ? Table.Name : Table.Alias, Name);
            }
        }

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

        public override string ToString()
        {
            return this.Name;
        }

        #region expressions

        public Expression Equal(Field other)
        {
            return new Equal(this, other);
        }

        public Expression Equal(object obj)
        {
            return new Equal(this, obj);
        }

        public Expression NotEqual(Field other)
        {
            return new NotEqual(this, other);
        }

        public Expression NotEqual(object obj)
        {
            return new NotEqual(this, obj);
        }

        public Expression LessThan(Field other)
        {
            return new LessThan(this, other);
        }

        public Expression LessThan(object other)
        {
            return new LessThan(this, other);
        }

        public Expression LessThanOrEqualTo(Field other)
        {
            return new LessThanOrEqualTo(this, other);
        }

        public Expression LessThanOrEqualTo(object other)
        {
            return new LessThanOrEqualTo(this, other);
        }

        public Expression GreaterThan(Field other)
        {
            return new GreaterThan(this, other);
        }

        public Expression GreaterThan(object other)
        {
            return new GreaterThan(this, other);
        }

        public Expression GreaterThanOrEqualTo(Field other)
        {
            return new GreaterThanOrEqualTo(this, other);
        }

        public Expression GreaterThanOrEqualTo(object other)
        {
            return new GreaterThanOrEqualTo(this, other);
        }

        public Expression Like(string expression_like)
        {
            return new Like(this, expression_like);
        }

        public Expression In(string[] sequence)
        {
            return new In(this, sequence);
        }

        public Expression In(object[] sequence)
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

        public static Expression operator ==
            (Field one, Field two)
        {
            return one.Equal(two);
        }

        public static Expression operator ==
            (Field one, object two)
        {
            return one.Equal(two);
        }

        public static Expression operator !=
            (Field one, Field two)
        {
            return one.NotEqual(two);
        }

        public static Expression operator !=
            (Field one, object two)
        {
            return one.NotEqual(two);
        }

        public static Expression operator <
            (Field one, Field two)
        {
            return one.LessThan(two);
        }

        public static Expression operator <
            (Field one, object two)
        {
            return one.LessThan(two);
        }

        public static Expression operator <=
            (Field one, Field two)
        {
            return one.LessThanOrEqualTo(two);
        }

        public static Expression operator <=
            (Field one, object two)
        {
            return one.LessThanOrEqualTo(two);
        }

        public static Expression operator >
            (Field one, Field two)
        {
            return one.GreaterThan(two);
        }

        public static Expression operator >
            (Field one, object two)
        {
            return one.GreaterThan(two);
        }

        public static Expression operator >=
            (Field one, Field two)
        {
            return one.GreaterThanOrEqualTo(two);
        }

        public static Expression operator >=
            (Field one, object two)
        {
            return one.GreaterThanOrEqualTo(two);
        }

        #endregion
    }
}
