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
        private IList<IProjection> _projects = new List<IProjection>();
        private IList<IJoin> _joins = new List<IJoin>();
        public string Name { get; set; }
        public string Alias { get; set; }

        public Table(string name)
        {
            Name = name;
            Alias = name;
        }

        public Table(string name, string alias)
        {
            Name = name;
            Alias = alias;
        }

        public Field this[string name]
        {
            get
            {
                var field = from f in _projects where f.Name == name select f;
                return field.Count() > 0 ? (Field)field.Single() : new Field(this, name);
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
            return String.Format("SELECT {0} {1}{2}", BuildSelect(), BuildFrom(), BuildJoin());
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

        private string BuildFrom()
        {
            if (Alias != Name)
            {
                return String.Format("FROM {0} AS {1}", Name, Alias);
            }
            else
            {
                return String.Format("FROM {0}", Name);
            }
        }

        #endregion
    }
}
