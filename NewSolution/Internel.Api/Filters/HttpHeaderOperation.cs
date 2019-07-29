using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using New.Common.Interceptors;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Internel.Api.Filters
{
    /// <summary>
    /// 在 Swagger UI 中显示自定义的 Header或者在url中增加query参数
    /// </summary>
    public class HttpHeaderOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<IParameter>();
            var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;
            var actionAttrs = context.ApiDescription.ActionAttributes();
            var isAuthorized = actionAttrs.Any(a => a.GetType() == typeof(AuthorizationInterceptor));
            if (isAuthorized == false)//提供action都没有权限特性标记，检查控制器有没有
            {
                var controllerAttrs = context.ApiDescription.ControllerAttributes();
                isAuthorized = controllerAttrs.Any(a => a.GetType() == typeof(AuthorizationInterceptor));
            }
            var isAllowAnonymouse = actionAttrs.Any(a => a.GetType() == typeof(AllowAnonymousAttribute));
            //先判断是否是匿名访问
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null && isAllowAnonymouse == false && isAuthorized)
            {
                var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                bool isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                //非匿名的方法,链接中添加accesstoken值
                if (!isAnonymous)
                {
                    operation.Parameters.Add(new NonBodyParameter()
                    {
                        Name = "companycode",
                        In = "query",//query header body path formData
                        Type = "string",
                        Required = true //是否必选
                    });
                    operation.Parameters.Add(new NonBodyParameter()
                    {
                        Name = "admdivcode",
                        In = "header",
                        Type = "int",
                        Required = false,
                        Description = "添加头部参数"
                    });
                }
            }
        }
    }
}
