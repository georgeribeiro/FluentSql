using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FluentQuery.Expressions;
using FluentQuery.Command;

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
        ITable Join(ITable table, IExpression expression);
        ITable LeftJoin(ITable table, IExpression expression);
        ITable RightJoin(ITable table, IExpression expression);
        ITable InnerJoin(ITable table, IExpression expression);
        ITable Where(IExpression expression);
        ITable GroupBy(Field field);
        ITable Insert(object values);
        ITable Update(object values);
        ITable Delete();
    }
}
