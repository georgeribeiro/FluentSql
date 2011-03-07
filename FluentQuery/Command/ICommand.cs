using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Expressions;
using FluentQuery.Clause;

namespace FluentQuery.Command
{
    public interface ICommand
    {
        string ToSql();
        ICommand Project(params Field[] fields);
        IJoin Join(ITable table);
        IJoin LeftJoin(ITable table);
        IJoin RightJoin(ITable table);
        IJoin InnerJoin(ITable table);
        ICommand Where(IExpression expression);
        ICommand GroupBy(Field field);
        ICommand Values(object values);
        IList<Field> Projects { get; set; }
        IList<IJoin> Joins { get; set; }
        IList<IExpression> Wheres { get; set; }
        IList<GroupBy> GroupBys { get; set; }
        IDictionary<string, object> FieldValues { get; set; }
    }
}
