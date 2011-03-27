using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Expressions;
using FluentSql.Clause;
using FluentSql.Aggregates;

namespace FluentSql.Command
{
    public interface ICommand
    {
        string ToSql();
        ICommand Project(params IProject[] fields);
        IJoin Join(ITable table);
        IJoin LeftJoin(ITable table);
        IJoin RightJoin(ITable table);
        IJoin InnerJoin(ITable table);
        ICommand Where(IExpression expression);
        ICommand OrderBy(params IOrder[] orders);
        ICommand GroupBy(params IGroup[] groups);
        ICommand Having(IExpression expression);
        ICommand Count();
        ICommand Values(object values);
        ICommand Top(int number);
        ICommand Distinct();
    }
}
