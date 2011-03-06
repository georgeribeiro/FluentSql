using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentQuery.Clause;
using FluentQuery.Expressions;
using System.Collections;

namespace FluentQuery.Command
{
    public class Select : ICommand
    {
        private IList<Field> Projects = new List<Field>();
        private IList<IJoin> Joins = new List<IJoin>();
        private IList<IExpression> Wheres = new List<IExpression>();
        private IList<GroupBy> GroupBys = new List<GroupBy>();

        protected ITable Table { get; set; }

        public Select(ITable table)
        {
            this.Table = table;
        }

        #region ICommand Members
        public virtual void Clear()
        {
            Projects.Clear();
            Joins.Clear();
            Wheres.Clear();
            GroupBys.Clear();
        }

        public virtual string ToSql()
        {
            return String.Format("SELECT {0} {1}{2}{3}{4}", BuildProject(), BuildFrom(), BuildJoin(), BuildWhere(), BuildGroupBy());
        }

        public virtual ICommand Project(params Field[] fields)
        {
            foreach (Field item in fields)
            {
                Projects.Add(item);
            }
            return this;
        }

        public virtual ICommand Join(ITable table, IExpression expression)
        {
            Joins.Add(new Join(table, expression));
            return this;
        }

        public virtual ICommand LeftJoin(ITable table, IExpression expresssion)
        {
            Joins.Add(new LeftJoin(table, expresssion));
            return this;
        }

        public virtual ICommand RightJoin(ITable table, IExpression expression)
        {
            Joins.Add(new RightJoin(table, expression));
            return this;
        }

        public virtual ICommand InnerJoin(ITable table, IExpression expression)
        {
            Joins.Add(new InnerJoin(table, expression));
            return this;
        }

        public virtual ICommand Where(IExpression expression)
        {
            Wheres.Add(expression);
            return this;
        }

        public virtual ICommand GroupBy(Field field)
        {
            GroupBys.Add(new GroupBy(field));
            return this;
        }

        public virtual ICommand Values(object values)
        {
            throw new NotSupportedException("Clause don't supported by command.");
        }
        #endregion

        #region Build Members
        protected string BuildProject()
        {
            if (Projects.Count == 0)
            {
                return "*";
            }
            return String.Join(", ", (from f in Projects select f.ToSql()).ToArray());
        }

        protected string BuildJoin()
        {
            if (Joins.Count > 0)
            {
                return " " + String.Join(" ", (from j in Joins select j.ToSql()).ToArray());
            }
            return String.Empty;
        }

        protected string BuildFrom()
        {
            if (!string.IsNullOrEmpty(Table.Alias))
            {
                return String.Format("FROM {0} AS {1}", Table.Name, Table.Alias);
            }
            return String.Format("FROM {0}", Table.Name);
        }

        protected string BuildWhere()
        {
            if (Wheres.Count > 0)
            {
                return " WHERE " + string.Join(" AND ", (from e in Wheres select e.ToSql()).ToArray());
            }
            return string.Empty;
        }

        protected string BuildGroupBy()
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
