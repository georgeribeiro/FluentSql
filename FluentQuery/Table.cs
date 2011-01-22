using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Clause;
using FluentQuery.Expression;

namespace FluentQuery
{
    public class Table
    {
        private IList<Field> _projects = new List<Field>();
        private IList<IJoin> _joins = new List<IJoin>();
        public string Name { get; set; }

        public Table(string name)
        {
            Name = name;
        }

        public Field this[string name]
        {
            get
            {
                return new Field(this, name);
            }
        }

        public Field All
        {
            get
            {
                return new Field(this, "*");
            }
        }

        #region Table Members

        public string ToSql()
        {
            return String.Format("SELECT {0} FROM {1}{2}", BuildSelect(), Name, BuildJoin());
        }

        public Table Project(params Field[] fields)
        {
            foreach (Field item in fields)
            {
                _projects.Add(item);
            }
            return this;
        }

        public Table Join(Table table, IExpression expression)
        {
            _joins.Add(new Join(table, expression));
            return this;
        }

        #endregion

        #region private members

        private string BuildSelect()
        {
            if (_projects.Count == 0)
            {
                return "*";
            }
            return String.Join(", ", (from f in _projects select f.ToSql()).ToArray());
        }

        private string BuildJoin()
        {
            if (_joins.Count > 0)
            {
                return " " + String.Join(" ", (from j in _joins select j.ToSql()).ToArray());
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion
    }
}
