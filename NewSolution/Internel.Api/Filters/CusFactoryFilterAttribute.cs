using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api.Filters
{
    /// <summary>
    /// 自定filter工厂（类似于SerivceFilter）
    /// </summary>
    public class CusFactoryFilterAttribute : Attribute, IFilterFactory//IFilterMetadata注入才会起作用
    {
        private Type filterType = null;
        public CusFactoryFilterAttribute(Type filterType)
        {
            this.filterType = filterType;
        }
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            Console.WriteLine("自定义filter工厂");
            return (IFilterMetadata)serviceProvider.GetService(filterType);
        }
    }
}
