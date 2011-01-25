using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Clause;
using FluentQuery.Expression;
using System.Collections;

namespace FluentQuery
{
    public class Table
    {
        private IList<Field> _projects = new List<Field>();
        private IList<IJoin> _joins = new List<IJoin>();
        private IList<ExpressionBase> _wheres = new List<ExpressionBase>();
        private readonly Hashtable _params = new Hashtable();
        public string Name { get; set; }
        public string Alias { get; set; }

        public Table(string name)
        {
            Name = name;
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

        internal string Param(string key, object obj)
        {
            string param = "";
            int count = 0;
            foreach (string k in _params.Keys)
            {
                if (k.Split('_')[0] + "_" + k.Split('_')[1] == key)
                    count++;
            }
            if (count > 0)
            {
                param = key + "_" + (++count).ToString();
                _params.Add(param, obj);
                return param;
            }
            else
            {
                param = key + "_1";
                _params.Add(param, obj);
                return param;
            }
        }

        public Hashtable Params
        {
            get
            {
                return _params;
            }
        }

        # region Methods

        public void Clear()
        {
            _projects.Clear();
            _joins.Clear();
            _wheres.Clear();
            _params.Clear();
        }

        #endregion

        #region Clauses Members

        public string ToSql()
        {
            return String.Format("SELECT {0} {1}{2}{3}", BuildSelect(), BuildFrom(), BuildJoin(), BuildWhere());
        }

        public Table Project(params Field[] fields)
        {
            foreach (Field item in fields)
            {
                _projects.Add(item);
            }
            return this;
        }

        public Table Join(Table table, ExpressionBase expression)
        {
            _joins.Add(new Join(table, expression));
            return this;
        }

        public Table LeftJoin(Table table, ExpressionBase expresssion)
        {
            _joins.Add(new LeftJoin(table, expresssion));
            return this;
        }

        public Table RightJoin(Table table, ExpressionBase expression)
        {
            _joins.Add(new RightJoin(table, expression));
            return this;
        }

        public Table InnerJoin(Table table, ExpressionBase expression)
        {
            _joins.Add(new InnerJoin(table, expression));
            return this;
        }

        public Table Where(ExpressionBase expression)
        {
            this._wheres.Add(expression);
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
            if (!string.IsNullOrEmpty(Alias))
            {
                return String.Format("FROM {0} AS {1}", Name, Alias);
            } 
            return String.Format("FROM {0}", Name);
        }

        private string BuildWhere()
        {
            if (_wheres.Count > 0)
            {
                return " WHERE " + string.Join(" AND ", (from e in _wheres select e.ToSql()).ToArray());
            }
            return string.Empty;
        }

        #endregion
    }
}
