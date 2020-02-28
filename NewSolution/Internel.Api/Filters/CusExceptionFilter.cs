using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api.Filters
{
    public class CusExceptionFilter : Attribute, IAsyncExceptionFilter
    {
        private readonly IHostingEnvironment env;
        public CusExceptionFilter(IHostingEnvironment env)
        {
            this.env = env;
        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var encryRequired=controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(EncryRequiredAttribute), false).LastOrDefault() as EncryRequiredAttribute;
            if (encryRequired!=null && encryRequired.isRequired)
            {
                Console.WriteLine("此action需要加密");
            }
            else
            {
                Console.WriteLine("此action不需要加密");
            }

            if (env.IsDevelopment())//模式
            {
                context.Result = new BadRequestObjectResult(new
                {
                    code = 500,
                    msg = context.Exception.Message+context.Exception.StackTrace
                });
            }
            else
            {
                context.Result = new BadRequestObjectResult(new
                {
                    code = 500,
                    msg = context.Exception.Message
                });
                context.ExceptionHandled = true;
            }
            Console.WriteLine("错误过滤器："+context.Exception.GetMessage());
            
           // context.Result = new InternalServerErrorObjectResult(response);
        }
    }
}
