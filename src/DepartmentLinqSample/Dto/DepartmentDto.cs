using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentLinqSample.Dto
{
    public struct DepartmentDto
    {
        public int Id { get; set; }         

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public string TelPhone { get; set; }
 
        public string Address { get; set; }

        public string Remark { get; set; }
         
        public int EmployeeNumber { get; set; }
    }
}
