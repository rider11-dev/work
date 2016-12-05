using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model
{
    public enum Relation
    {
        Contain,
        StartWith,
        EndWith,
        GreaterThan,
        GreaterOrEqual,
        LessThan,
        LessOrEqual,
        Equal,
        In,
        IsEmpty,
        Between
    }
}
