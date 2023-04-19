using System.Linq;
using System.Reflection;
using TechnicalTest.Data.Model;

namespace TechnicalTest.API
{
    public static class template
    {
        //te
        public static void Template()
        {
            Type c = typeof(Customer);
            //PropertyInfo[] ps = c.GetProperties();
            //string asString = string.Join("\n", ps.Select(x => $"//{x.Name}"));
        }
    }
}
