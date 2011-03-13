using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Clause;
using FluentSql.Expressions;
using System.Collections;
using FluentSql.Command;

namespace FluentSql
{
    public class Table : ITable
    {
        private ICommand _command = null;
        private readonly Hashtable _params = new Hashtable();
        private IList<Field> _fields = new List<Field>();
        public string Name { get; private set; }
        public string Alias { get; private set; }

        public Table(string name)
        {
            this.Name = name;
            _command = new Select(this);
        }

        public Table(string name, string alias)
        {
            Name = name;
            Alias = alias;
            this._command = new Select(this);
        }

        public Field this[string name]
        {
            get
            {
                var fields = from f in _fields where f.Name == name select f;
                if (fields.Count() > 0)
                {
                    return fields.Single();
                }
                else
                {
                    Field f = new Field(this, name);
                    _fields.Add(f);
                    return f;
                }
            }
        }

        public Field All
        {
            get
            {
                return this["*"];
            }
        }

        public string AddParam(string key, object obj)
        {
            string param = "";
            string format_param = "{0}_{1}_{2}";
            int count = 0;
            foreach (string k in _params.Keys)
            {
                var list = k.Replace(this.Name, "").Split('_').ToList();
                list.RemoveAt(0);
                list.RemoveAt(list.Count - 1);
                string kk = string.Join("_", list.ToArray());
                if (kk == key)
                    count++;
            }
            param = String.Format(format_param, Name, key, (count > 0 ? (++count) : 1).ToString());
            _params.Add(param, obj);
            return param;
        }

        public string ToSql()
        {
            return _command.ToSql();
        }

        public Hashtable Params
        {
            get
            {
                return _params;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        # region Methods
        public void Clear()
        {
            _command = new Select(this);
        }
        #endregion

        #region ITable members
        public ITable Project(params IProject[] projects)
        {
            _command.Project(projects);
            return this;
        }

        public IJoin Join(ITable table)
        {
            IJoin j = _command.Join(table);
            return j;
        }

        public IJoin LeftJoin(ITable table)
        {
            IJoin j = _command.LeftJoin(table);
            return j;
        }

        public IJoin RightJoin(ITable table)
        {
            IJoin j = _command.RightJoin(table);
            return j;
        }

        public IJoin InnerJoin(ITable table)
        {
            IJoin j = _command.InnerJoin(table);
            return j;
        }

        public ITable Where(IExpression expression)
        {
            _command.Where(expression);
            return this;
        }

        public ITable OrderBy(params IOrder[] order)
        {
            _command.OrderBy(order);
            return this;
        }

        public ITable GroupBy(params IGroup[] groups)
        {
            _command.GroupBy(groups);
            return this;
        }

        public ITable Having(IExpression expression)
        {
            _command.Having(expression);
            return this;
        }

        public ITable Count()
        {
            _command.Count();
            return this;
        }

        public ITable Top(int number)
        {
            _command.Top(number);
            return this;
        }

        public ITable Insert(object values)
        {
            _command = new Insert(this);
            _command.Values(values);
            return this;
        }

        public ITable Update(object values)
        {
            _command = new Update(this, ((Select)_command).Wheres);
            _command.Values(values);
            return this;
        }

        public ITable Delete()
        {
            _command = new Delete(this, ((Select)_command).Wheres);
            return this;
        }
        #endregion
    }
}
