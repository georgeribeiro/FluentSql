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

        protected string FieldToString(Field f)
        {
            return string.IsNullOrEmpty(f.Alias) ? f.Project : f.Alias;
        }

        protected OperatorBase() { }

        public OperatorBase(Field one, Field two)
        {
            One = FieldToString(one);
            Two = FieldToString(two);
        }

        public OperatorBase(Field one, string two)
        {
            One = FieldToString(one);
            string param = one._table.Param(String.Format("{0}_{1}", one.Table.Name, one.Name), two);
            Two = "@" + param;
        }

        public OperatorBase(Field one, int two)
        {
            One = FieldToString(one);
            string param = one._table.Param(String.Format("{0}_{1}", one.Table.Name, one.Name), two);
            Two = "@" + param;
        }

        public OperatorBase(Field one, decimal two)
        {
            One = FieldToString(one);
            string param = one._table.Param(String.Format("{0}_{1}", one.Table.Name, one.Name), two);
            Two = "@" + param;
        }

        public OperatorBase(Field one, DateTime two)
        {
            One = FieldToString(one);
            string param = one._table.Param(String.Format("{0}_{1}", one.Table.Name, one.Name), two);
            Two = "@" + param;
        }

        public override string ToString()
        {
            return ToSql();
        }
    }
}
