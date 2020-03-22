using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using tut2.Models;

namespace tut2
{
    public class CustomComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals($"{x.IndexNumber} {x.Email} {x.FirstName} {x.LastName}",$"{y.IndexNumber} {y.Email} {y.FirstName} {y.LastName}");
        }

        public int GetHashCode(Student obj)
        {
            return StringComparer.CurrentCultureIgnoreCase.GetHashCode($"{obj.IndexNumber} {obj.Email} {obj.FirstName} {obj.LastName}");
        }
    }
}
