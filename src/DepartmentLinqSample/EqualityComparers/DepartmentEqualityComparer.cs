using DepartmentLinqSample.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DepartmentLinqSample.EqualityComparers
{
    public class DepartmentEqualityComparer : IEqualityComparer<DepartmentDto>
    {
        public bool Equals([AllowNull] DepartmentDto x, [AllowNull] DepartmentDto y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] DepartmentDto obj)
        {

            return obj.ToString().GetHashCode();

        }
    }
}
