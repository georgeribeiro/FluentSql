using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Clause
{
    public interface IOrder
    {
        string AsOrder();
        IOrder Asc { get; }
        IOrder Desc { get; }
        TypeOrder Order { get; }
    }
}
