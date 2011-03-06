using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Clause;
using FluentQuery.Expressions;
using System.Collections;
using FluentQuery.Command;

namespace FluentQuery
{
    public class Table<T> : ITable where T:ICommand
    {
        private ICommand _command = null;
        private readonly Hashtable _params = new Hashtable();
        private IList<Field> _fields = new List<Field>();

        public string Name { get; set; }
        public string Alias { get; set; }

        private void InitializeCommand() 
        {
            if (typeof(T) == typeof(Select))
            {
                _command = new Select(this);
            }
            else if (typeof(T) == typeof(Update))
            {
                _command = new Update(this);
            }
            else if (typeof(T) == typeof(Insert))
            {
                _command = new Insert(this);
            }
            else if (typeof(T) == typeof(Delete))
            {
                _command = new Delete(this);
            }
        }

        public Table(string name)
        {
            this.Name = name;
            InitializeCommand();
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
            InitializeCommand();
        }

        #endregion

        #region Select members

        public ITable Project(params Field[] fields)
        {
            _command.Project(fields);
            return this;
        }

        public ITable Join(ITable table, IExpression expression)
        {
            _command.Join(table, expression);
            return this;
        }

        public ITable LeftJoin(ITable table, IExpression expresssion)
        {
            _command.LeftJoin(table, expresssion);
            return this;
        }

        public ITable RightJoin(ITable table, IExpression expression)
        {
            _command.RightJoin(table, expression);
            return this;
        }

        public ITable InnerJoin(ITable table, IExpression expression)
        {
            _command.InnerJoin(table, expression);
            return this;
        }

        public ITable Where(IExpression expression)
        {
            _command.Where(expression);
            return this;
        }

        public ITable GroupBy(Field field)
        {
            _command.GroupBy(field);
            return this;
        }

        #endregion

        public ITable Insert(object values)
        {
            _command.Values(values);
            return this;
        }

        public ITable Update(object values)
        {
            _command.Values(values);
            return this;
        }

        public ITable Delete()
        {
            throw new NotImplementedException();
        }
    }
}
