﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSql.Clause;
using FluentSql.Expressions;
using System.Collections;
using FluentSql.Aggregates;

namespace FluentSql.Command
{
    internal class Select : ICommand
    {
        public Select(ITable table)
        {
            this.Table = table;
            this.Projects = new List<IProject>();
            this.Joins = new List<IJoin>();
            this.Wheres = new List<IExpression>();
            this.Orders = new List<IOrder>();
            this.GroupBys = new List<IGroup>();
            this.Havings = new List<IExpression>();
            this._Count = false;
            this._Top = 0;
        }

        #region ICommand Members
        public IList<IProject> Projects { get; set; }
        public IList<IJoin> Joins { get; set; }
        public IList<IExpression> Wheres { get; set; }
        public IList<IOrder> Orders { get; set; }
        public IList<IGroup> GroupBys { get; set; }
        public IList<IExpression> Havings { get; set; }
        public ITable Table { get; set; }
        public bool _Count { get; set; }
        public int _Top { get; set; }
        
        public string ToSql()
        {
            return String.Format("SELECT {0}{1} {2}{3}{4}{5}{6}{7}", BuildTop(), BuildProject(), 
                BuildFrom(), BuildJoin(), BuildWhere(), BuildOrderBy(), BuildGroupBy(), BuildHaving());
        }

        public ICommand Project(params IProject[] projects)
        {
            foreach (var p in projects)
            {
                Projects.Add(p);
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

        public ICommand GroupBy(params IGroup[] groups)
        {
            foreach (IGroup g in groups)
            {
                GroupBys.Add(g);    
            }
            return this;
        }

        public ICommand Having(IExpression expression)
        {
            Havings.Add(expression);
            return this;
        }

        public ICommand Count()
        {
            this._Count = true;
            return this;
        }

        public ICommand Top(int number)
        {
            this._Top = number;
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
            if (this._Count)
            {
                return "COUNT(*)";
            }
            if (Projects.Count == 0)
            {
                return "*";
            }
            return String.Join(", ", (from f in Projects select f.AsProject()).ToArray());
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

        public string BuildOrderBy()
        {
            if (Orders.Count > 0)
            {
                return " ORDER BY " + string.Join(" AND ", (from o in Orders select o.AsOrder()).ToArray());
            }
            return string.Empty;
        }

        private string BuildGroupBy()
        {
            if (GroupBys.Count > 0)
            {
                return " GROUP BY " + String.Join(", ", (from g in GroupBys select g.AsGroup()).ToArray());
            }
            return string.Empty;
        }

        public string BuildHaving()
        {
            if (Havings.Count > 0)
            {
                return " HAVING " + string.Join(" AND ", (from h in Havings select h.ToSql()).ToArray());
            }
            return string.Empty;
        }

        public string BuildTop()
        {
            if (_Top > 0)
            {
                return string.Format("TOP {0} ", _Top);
            }
            return string.Empty;
        }
        #endregion

        #region ICommand Members

        public ICommand OrderBy(params IOrder[] orders)
        {
            foreach (IOrder order in orders)
            {
                Orders.Add(order);
            }
            return this;
        }

        #endregion
    }
}
