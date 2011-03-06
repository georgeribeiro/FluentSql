using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expressions;

namespace FluentQuery.Command
{
    public interface ICommand
    {
        string ToSql();
        void Clear();
        ICommand Project(params Field[] fields);
        ICommand Join(ITable table, IExpression expression);
        ICommand LeftJoin(ITable table, IExpression expression);
        ICommand RightJoin(ITable table, IExpression expression);
        ICommand InnerJoin(ITable table, IExpression expression);
        ICommand Where(IExpression expression);
        ICommand GroupBy(Field field);
        ICommand Values(object values);
    }
}
