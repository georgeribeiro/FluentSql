﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQuery.Clause
{
    public interface IClause
    {
        string ToSql();
    }
}
