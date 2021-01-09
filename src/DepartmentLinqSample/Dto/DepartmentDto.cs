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


    public class DepartmentRequest
    {
        public DepartmentRequest()
        {
            // 默认就给Itmes赋予一个默认值(没有任何元素的空集合),避免后续代码中直接对该集合操作时候出现空引用异常
            Items = new List<DepartTagDto>();
        }       

        public List<DepartTagDto> Items { get; set; }
    }


    public class DepartTagDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
