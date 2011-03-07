using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FluentQuery.Expressions;
using FluentQuery.Command;
using FluentQuery.Clause;

namespace FluentQuery
{
    public interface ITable
    {
        string ToSql();
        string Name { get; set; }
        string Alias { get; set; }
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
