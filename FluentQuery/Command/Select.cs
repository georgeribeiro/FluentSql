using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Clause;
using FluentQuery.Expressions;
using System.Collections;

namespace FluentQuery.Command
{
    internal class Select : ICommand
    {
        public Select(ITable table)
        {
            this.Table = table;
            this.Projects = new List<Field>();
            this.Joins = new List<IJoin>();
            this.Wheres = new List<IExpression>();
            this.GroupBys = new List<GroupBy>();
        }

        #region ICommand Members
        public IList<Field> Projects { get; set; }
        public IList<IJoin> Joins { get; set; }
        public IList<IExpression> Wheres { get; set; }
        public IList<GroupBy> GroupBys { get; set; }
        public IDictionary<string, object> FieldValues
        {
            get
            {
                throw new NotSupportedException("Clause don't supported by command.");   
            }
            set
            {
                throw new NotSupportedException("Clause don't supported by command.");
            }
        }
        public ITable Table { get; set; }
        
        public string ToSql()
        {
            return String.Format("SELECT {0} {1}{2}{3}{4}", BuildProject(), BuildFrom(), BuildJoin(), BuildWhere(), BuildGroupBy());
        }

        public ICommand Project(params Field[] fields)
        {
            foreach (Field item in fields)
            {
                Projects.Add(item);
            }
            return this;
        }

        public IJoin Join(ITable table)
        {
            IJoin j = new Join(Table, table);
            Joins.Add(j);
            return j;
        }

        public IJoin LeftJoin(ITable table)
        {
            IJoin j = new LeftJoin(Table, table);
            Joins.Add(j);
            return j;
        }

        public IJoin RightJoin(ITable table)
        {
            IJoin j = new RightJoin(Table, table);
            Joins.Add(j);
            return j;
        }

        public IJoin InnerJoin(ITable table)
        {
            IJoin j = new InnerJoin(Table, table);
            Joins.Add(j);
            return j;
        }

        public ICommand Where(IExpression expression)
        {
            Wheres.Add(expression);
            return this;
        }

        public ICommand GroupBy(Field field)
        {
            GroupBys.Add(new GroupBy(field));
            return this;
        }

        public ICommand Values(object values)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }
        #endregion

        #region Build Members
        private string BuildProject()
        {
            if (Projects.Count == 0)
            {
                return "*";
            }
            return String.Join(", ", (from f in Projects select f.ToSql()).ToArray());
        }

        private string BuildJoin()
        {
            if (Joins.Count > 0)
            {
                return " " + String.Join(" ", (from j in Joins select j.ToSql()).ToArray());
            }
            return String.Empty;
        }

        private string BuildFrom()
        {
            if (!string.IsNullOrEmpty(Table.Alias))
            {
                return String.Format("FROM {0} AS {1}", Table.Name, Table.Alias);
            }
            return String.Format("FROM {0}", Table.Name);
        }

        private string BuildWhere()
        {
            if (Wheres.Count > 0)
            {
                return " WHERE " + string.Join(" AND ", (from e in Wheres select e.ToSql()).ToArray());
            }
            return string.Empty;
        }

        private string BuildGroupBy()
        {
            if (GroupBys.Count > 0)
            {
                return " GROUP BY " + String.Join(", ", (from g in GroupBys select g.ToSql()).ToArray());
            }
            return string.Empty;
        }
        #endregion
    }
}
