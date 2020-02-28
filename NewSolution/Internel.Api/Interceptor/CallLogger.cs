using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api.Interceptor
{
    public class CallLogger : IInterceptor
    {
        public bool AllowMultiple => true;

        public bool Inherited { get; set; }
        public int Order { get; set; }

        public async Task Invoke(AspectContext context, AspectDelegate next)
        {
            await next(context);
        }
    }
}
