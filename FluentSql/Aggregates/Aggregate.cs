using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Expressions;

namespace FluentSql.Aggregates
{
    public class Aggregate : IAggregate, IStatement
    {
        protected Field Field { get; set; }
        protected virtual string Agg
        {
            get
            {
                return Field.Project;
            }
        }

        public Aggregate(Field aggregate)
        {
            Field = aggregate;
        }

        public Aggregate() { }

        public IAggregate As(string alias)
        {
            Alias = alias;
            return this;
        }

        protected string BuildAgg()
        {
            return string.Format("{0}({1})", this.GetType().Name.ToUpper(), Agg);
        }

        public string ToSql()
        {
            if (!string.IsNullOrEmpty(Alias))
            {
                return String.Format("{0} AS {1}", BuildAgg(), Alias);
            }
            else
            {
                return String.Format("{0}", BuildAgg());
            }
        }

        #region IStatement Members

        public string Project
        {
            get 
            {
                return BuildAgg();
            }
        }

        public string Alias
        {
            get;
            private set;
        }

        public string Name
        {
            get 
            {
                return String.Format("{0}_{1}", this.GetType().Name.ToLower(), Field.Name);
            }
        }

        public ITable Table
        {
            get 
            {
                return Field.Table;
            }
        }

        public Expression Equal(IStatement other)
        {
            return new Equal(this, other);
        }

        public Expression Equal(object obj)
        {
            return new Equal(this, obj);
        }

        public Expression NotEqual(IStatement other)
        {
            return new NotEqual(this, other);
        }

        public Expression NotEqual(object obj)
        {
            return new NotEqual(this, obj);
        }

        public Expression LessThan(IStatement other)
        {
            return new LessThan(this, other);
        }

        public Expression LessThan(object obj)
        {
            return new LessThan(this, obj);
        }

        public Expression LessThanOrEqualTo(IStatement other)
        {
            return new LessThanOrEqualTo(this, other);
        }

        public Expression LessThanOrEqualTo(object obj)
        {
            return new LessThanOrEqualTo(this, obj);
        }

        public Expression GreaterThan(IStatement other)
        {
            return new GreaterThan(this, other);
        }

        public Expression GreaterThan(object obj)
        {
            return new GreaterThan(this, obj);
        }

        public Expression GreaterThanOrEqualTo(IStatement other)
        {
            return new GreaterThanOrEqualTo(this, other);
        }

        public Expression GreaterThanOrEqualTo(object obj)
        {
            return new GreaterThanOrEqualTo(this, obj);
        }

        public Expression Like(string expression_like)
        {
            return new Like(this, expression_like); 
        }

        public Expression In(params string[] sequence)
        {
            return new In(this, sequence);
        }

        public Expression In(params object[] sequence)
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
            (Aggregate one, IStatement two)
        {
            return one.Equal(two);
        }

        public static Expression operator ==
            (Aggregate one, object two)
        {
            return one.Equal(two);
        }

        public static Expression operator !=
            (Aggregate one, IStatement two)
        {
            return one.NotEqual(two);
        }

        public static Expression operator !=
            (Aggregate one, object two)
        {
            return one.NotEqual(two);
        }

        public static Expression operator <
            (Aggregate one, IStatement two)
        {
            return one.LessThan(two);
        }

        public static Expression operator <
            (Aggregate one, object two)
        {
            return one.LessThan(two);
        }

        public static Expression operator <=
            (Aggregate one, IStatement two)
        {
            return one.LessThanOrEqualTo(two);
        }

        public static Expression operator <=
            (Aggregate one, object two)
        {
            return one.LessThanOrEqualTo(two);
        }

        public static Expression operator >
            (Aggregate one, IStatement two)
        {
            return one.GreaterThan(two);
        }

        public static Expression operator >
            (Aggregate one, object two)
        {
            return one.GreaterThan(two);
        }

        public static Expression operator >=
            (Aggregate one, IStatement two)
        {
            return one.GreaterThanOrEqualTo(two);
        }

        public static Expression operator >=
            (Aggregate one, object two)
        {
            return one.GreaterThanOrEqualTo(two);
        }
        #endregion

        #region IStatement Members


        public Expression In(ITable table)
        {
            return new In(this, table);
        }

        #endregion
    }
}
