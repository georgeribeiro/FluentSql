using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expressions
{
    public class OperatorBase : Expression
    {
        protected string One { get; set; }
        protected string Two { get; set; }

        protected string FieldToString(Field field)
        {
            return string.IsNullOrEmpty(field.Alias) ? field.Project : field.Alias;
        }

        protected OperatorBase() { }

        public OperatorBase(Field one, Field two)
        {
            One = FieldToString(one);
            try
            {
                Two = FieldToString(two);
            }
            catch (NullReferenceException)
            {
                Two = null;
            }
        }

        public OperatorBase(Field one, object two)
        {
            One = FieldToString(one);
            string param = one.Table.AddParam(String.Format("{0}_{1}", one.Table.Name, one.Name), two);
            Two = "@" + param;
        }

        public override string ToString()
        {
            return ToSql();
        }
    }
}
