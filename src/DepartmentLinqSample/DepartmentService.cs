using DepartmentLinqSample.Dto;
using DepartmentLinqSample.EqualityComparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DepartmentLinqSample
{
    public class DepartmentService
    {
        public List<DepartmentDto> InitDepartmentData()
        {
            List<DepartmentDto> lst = new List<DepartmentDto>();
            lst.AddRange(new DepartmentDto[] {
                new DepartmentDto() {
                     Address ="一马路XX号",
                     Id=1,
                     Name="一级一号科室",
                     Remark="",
                     TelPhone="0731-6111111",
                     EmployeeNumber=3,
                },
                new DepartmentDto() {
                     Address ="二马路XX号",
                     Id=2,
                     Name="一级二号科室",
                     Remark="",
                     TelPhone="0731-6111111",
                     EmployeeNumber=4,
                },
                new DepartmentDto() {
                     Address ="三马路XX号",
                     Id=3,
                     Name="一级三号科室",
                     Remark="",
                     TelPhone="0731-6222222",
                     EmployeeNumber=6,
                },
                new DepartmentDto() {
                     Address ="一马路XX号",
                     ParentId=1,
                     Id=4,
                     Name="二级一号科室",
                     Remark="",
                     TelPhone="0731-6222222",
                     EmployeeNumber=7,
                },
                new DepartmentDto() {
                     Address ="二马路XX号",
                     ParentId=2,
                     Id=5,
                     Name="二级二号科室",
                     Remark="",
                     TelPhone="0731-6222222",
                     EmployeeNumber=5,
                },
            });
            return lst;
        }

        public void ConvertListTest()
        {
            try
            {
                object[] ss = { 1, "2", 3, "四", "五", "7" };
                // 1、3 OfType的本质是循环集合，对每个集合项进行类型验证(不是强转,所以此处的结果是1、3 而不是1、2、3、7)
                var lst = ss.ToList().OfType<int>().ToList();
                // 3  
                int max = ss.OfType<int>().Max();
                // 这句代码会提示“System.InvalidCastException:“Unable to cast object of type 'System.String' to type 'System.Int32'.”异常，原因:Cast的执行效率会略高与OfType,因为在对集合项进行类型转换前不会对其进行类型校验,当你确保集合的类型是安全的则可以用Cast,但是能用到Cast和OfType的时候基本上都是用OfType了..
                int maxCast = ss.Cast<int>().Max();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public List<DepartmentDto> GetNotExistsParentDepartmentItems()
        {
            List<DepartmentDto> lstDepartItems = InitDepartmentData();
            //1、获取未存在父级科室的科室集合 1、2、3            
            List<DepartmentDto> notExistsParentDepartmentIdLst = lstDepartItems     
                .Where(p => !p.ParentId.HasValue)
                .ToList();
            // 不使用Linq方式
            List<DepartmentDto> notExistsParentDepartmentIdLst_1 = new List<DepartmentDto>();
            foreach (DepartmentDto department in lstDepartItems)
            {
                if (!department.ParentId.HasValue)
                    notExistsParentDepartmentIdLst_1.Add(department);
            }

            //2、获取存在子科室的科室集合 1、2
            List<DepartmentDto> existsParentDepartmentIdLst1 = lstDepartItems
                .Where(p => lstDepartItems.Select(k => k.ParentId).Contains(p.Id))
                .ToList();
            // 不使用Linq方式
            List<DepartmentDto> existsParentDepartmentIdLst1_1 = new List<DepartmentDto>();
            foreach (DepartmentDto parentDepart in lstDepartItems)
            {
                foreach (DepartmentDto childDepart in lstDepartItems) {
                    if (parentDepart.Id == childDepart.ParentId)
                    {
                        existsParentDepartmentIdLst1_1.Add(parentDepart);
                        continue;
                    }
                }
            }

            //3、获取存在富可视的集合 4、5
            List<DepartmentDto> existsParentDepartmentIdLst3 = lstDepartItems.Where(p => p.ParentId.HasValue)
                .ToList();
            //4、获取ID为1和2的科室集合
            List<DepartmentDto> existsParentDepartmentIdLst4 = lstDepartItems.Where(p =>(new int[] { 1, 2 }).Contains(p.Id)).ToList();
            //5、获取ID不为1和2的科室集合
            List<DepartmentDto> existsParentDepartmentIdLst5 = lstDepartItems.Where(p => !(new int[] { 1, 2 }).Contains(p.Id)).ToList();


            //6、分组
            var groupDto = lstDepartItems
                .GroupBy(p => p.Address)
                .Select(p => new
                {
                    Address = p.Key,
                    SumEmployeeCount = p.Sum(p => p.EmployeeNumber),
                    AvgEmployeeCount = p.Average(p => p.EmployeeNumber),
                }).ToList();
            // 结果 Address=一马路XX号,SumEmployeeCount=10 | Address=二马路XX号,SumEmployeeCount=9 | Address=三马路XX号,SumEmployeeCount=6 
 

            // 获取两个集合不相等的元素
            List<DepartmentDto> lstDepartItemsCPs = InitDepartmentData();
            lstDepartItemsCPs.Add(new DepartmentDto()
            {
                Address = "三马路XX号",
                Id = 6,
                Name = "二级三号科室",
                Remark = "",
                TelPhone = "0731-6222222",
                EmployeeNumber = 7
            });


            // 这里如果DepartmentDto为引用类型(class)则需要使用比较器DepartmentEqualityComparer才能返回我们的预期值(根据ID值判断是否相等)
            List<DepartmentDto> diffList = lstDepartItemsCPs.Except(lstDepartItems, new DepartmentEqualityComparer()).ToList();
            // 获取相等元素
            List<DepartmentDto> diffList1 = lstDepartItemsCPs.Intersect(lstDepartItems.Select(p => p), new DepartmentEqualityComparer()).ToList();
            // 需要添加IEqualityComparer，因为集合内的内容为引用类型！所以两个集合的“值”是不同的，引用类型的值在这里还包含了指向的内存堆的引用地址
            bool isEqual = lstDepartItems.SequenceEqual(InitDepartmentData(), new DepartmentEqualityComparer());













            // 这里如果DepartmentDto为引用类型(class)则需要使用比较器DepartmentEqualityComparer才能返回我们的预期值(根据ID值判断是否相等)
            List<DepartmentDto> diffList = lstDepartItemsCPs.Except(lstDepartItems, new DepartmentEqualityComparer()).ToList();
            // 获取相等元素
            List<DepartmentDto> diffList1 = lstDepartItemsCPs.Intersect(lstDepartItems.Select(p => p), new DepartmentEqualityComparer()).ToList();

            // 这里如果DepartmentDto为值类型(struct)则不需要使用比较器DepartmentEqualityComparer即可返回我们的预期值(根据ID值判断是否相等)
            List<DepartmentDto> diffList3 = lstDepartItemsCPs.Except(lstDepartItems).ToList();
            // 获取相等元素
            List<DepartmentDto> diffList4 = lstDepartItemsCPs.Intersect(lstDepartItems.Select(p => p)).ToList();

            // 需要添加IEqualityComparer，因为集合内的内容为引用类型！所以两个集合的“值”是不同的，引用类型的值在这里还包含了指向的内存堆的引用地址
            bool isEqual = lstDepartItems.SequenceEqual(InitDepartmentData(), new DepartmentEqualityComparer());
            // 如果把DepartmentDto的类型改为值类型则可以不需要IEqualityComparer进行判断的结果也会为true
            isEqual = lstDepartItems.SequenceEqual(InitDepartmentData());
            // 集合的转换测试 
            ConvertListTest();
            return notExistsParentDepartmentIdLst;
        }
    }
}
