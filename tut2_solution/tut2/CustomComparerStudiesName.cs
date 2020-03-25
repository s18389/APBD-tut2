using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using tut2.Models;

namespace tut2
{
    public class CustomComparerStudiesName : IEqualityComparer<StudiesList>
    {
        public bool Equals(StudiesList x, StudiesList y)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals($"{x.name}",$"{y.name}");
        }

        public int GetHashCode(StudiesList obj)
        {
            return StringComparer.CurrentCultureIgnoreCase.GetHashCode($"{obj.name}");
        }
    }
}
