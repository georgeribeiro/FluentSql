using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FluentSql.Expressions;
using FluentSql.Command;
using FluentSql.Clause;

namespace FluentSql
{
    public interface ITable
    {
        string ToSql();
        string Name { get; }
        string Alias { get; }
        string AddParam(string key, object obj);
        Hashtable Params { get; }
        void Clear();
        ITable Project(params Field[] fields);
        IJoin Join(ITable table);
        IJoin LeftJoin(ITable table);
        IJoin RightJoin(ITable table);
        IJoin InnerJoin(ITable table);
        ITable Where(IExpression expression);
        ITable GroupBy(Field field);
        ITable Insert(object values);
        ITable Update(object values);
        ITable Delete();
    }
}
