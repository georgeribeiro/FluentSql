using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Expressions
{
    public interface IStatement
    {
        string Project { get; }
        string Alias { get; }
        string Name { get; }
        ITable Table { get; }
        Expression Equal(IStatement other);
        Expression Equal(object obj);
        Expression NotEqual(IStatement other);
        Expression NotEqual(object obj);
        Expression LessThan(IStatement other);
        Expression LessThan(object obj);
        Expression LessThanOrEqualTo(IStatement other);
        Expression LessThanOrEqualTo(object obj);
        Expression GreaterThan(IStatement other);
        Expression GreaterThan(object obj);
        Expression GreaterThanOrEqualTo(IStatement other);
        Expression GreaterThanOrEqualTo(object obj);
        Expression Like(string expression_like);
        Expression In(params string[] sequence);
        Expression In(params object[] sequence);
        Expression In(ITable table);
        Not Not { get; }
    }
}
