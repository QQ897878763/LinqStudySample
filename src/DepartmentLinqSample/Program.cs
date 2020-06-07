using System;
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
    }
}
