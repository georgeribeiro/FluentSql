﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Aggregates
{
    public interface IAggregate : IProject
    {
        IAggregate As(string alias);
    }
}
