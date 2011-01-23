using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Expression
{
    public class OperatorBase : ExpressionBase
    {
        protected string One { get; set; }
        protected string Two { get; set; }

        private string FieldToString(Field f)
        {
            return string.IsNullOrEmpty(f.Alias) ? f.Project : f.Alias;
        }

        public OperatorBase(Field one, Field two)
        {
            One = FieldToString(one);
            Two = FieldToString(two);
        }

        public OperatorBase(Field one, string two)
        {
            One = FieldToString(one);
            Two = String.Format("'{0}'", two);
        }

        public OperatorBase(Field one, int two)
        {
            One = FieldToString(one);
            Two = two.ToString();
        }

        public OperatorBase(Field one, decimal two)
        {
            One = FieldToString(one);
            Two = two.ToString();
        }

        public OperatorBase(Field one, DateTime two)
        {
            One = FieldToString(one);
            Two = two.ToString();
        }
    }
}
