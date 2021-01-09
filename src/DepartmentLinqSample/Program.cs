using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace DepartmentLinqSample
{
    class Program
    {
        static void Main(string[] args)
        {            
            Test();
            //TestDepart();
        }

        private static void TestDepart()
        {
            Console.WriteLine("***********获取没有父级科室的科室列表!****************");
            JsonSerializerOptions opt = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            DepartmentService service = new DepartmentService();
            var lst = service.GetNotExistsParentDepartmentItems();
            foreach (var item in lst)
            {
                Console.WriteLine(JsonSerializer.Serialize(item, opt));
            }
        }
        private static void Test()
        {
            try
            {
                List<int> items = new List<int>();
                var a = items?.FirstOrDefault();
                var b = items?.First();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
