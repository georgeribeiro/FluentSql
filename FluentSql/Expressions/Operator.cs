using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    public class Operator : Expression
    {
        protected string One { get; set; }
        protected string Two { get; set; }

        protected string StatementToString(IStatement statement)
        {
            return string.IsNullOrEmpty(statement.Alias) ? statement.Project : statement.Alias;
        }

        protected Operator() { }

        public Operator(IStatement one, IStatement two)
        {
            One = StatementToString(one);
            try
            {
                Two = StatementToString(two);
            }
            catch (NullReferenceException)
            {
                Two = null;
            }
        }

        public Operator(IStatement one, object two)
        {
            One = StatementToString(one);
            if (two != null)
            {
                if (two is ITable)
                {
                    Two = ((ITable)two).ToSql();
                }
                else
                {
                    string param = one.Table.AddParam(one.Name, two);
                    Two = "@" + param;
                }
            }
            else
            {
                Two = null;
            }
        }

        public override string ToString()
        {
            return ToSql();
        }
    }
}
