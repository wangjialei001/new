using AspectCore.DynamicProxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
namespace New.Common.Interceptors
{
    [NonAspect]
    public class AuthorizationInterceptor : AbstractInterceptorAttribute
    {
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                var parameter = context.Parameters.FirstOrDefault<dynamic>();

                if (parameter == null)
                {
                    throw new Exception("参数为空！");
                }

                var controller = (ControllerBase)context.Proxy;
                foreach (var item in controller.Request.Headers)
                {
                    Console.WriteLine($"Key:{item.Key}, Value:{item.Value}");
                }
                controller.Request.Headers.TryGetValue("companycode", out StringValues companyCode);
                controller.Request.Headers.TryGetValue("admdivcode", out StringValues admDivCode);
                if (string.IsNullOrWhiteSpace(companyCode))
                {
                    throw new Exception("companyCode is blank in header！");
                }
                if (string.IsNullOrWhiteSpace(admDivCode))
                {
                    throw new Exception("admDivCode is blank in header！");
                }

                Console.WriteLine($"接收到的参数为：{JsonConvert.SerializeObject(parameter)}！");
                Console.WriteLine($"接收到的companyCode：{companyCode},admDivCode:{admDivCode}！");

                if (companyCode.ToString() != parameter.CompanyCode && admDivCode != parameter.AdmDivCode.ToString())
                {
                    throw new Exception("参数验证失败！");
                }

                foreach (var item in context.Parameters)
                {
                    Console.WriteLine("接收到的参数：");
                    Console.WriteLine(JsonConvert.SerializeObject(item));
                }
                await next(context);//执行被拦截的方法
                string invokeResult = string.Empty;
                if (IsPropertyExist((dynamic)context.ReturnValue, "Result"))
                {
                    invokeResult = JsonConvert.SerializeObject(((dynamic)context.ReturnValue).Result);
                }
                else
                {
                    invokeResult = JsonConvert.SerializeObject(context.ReturnValue);
                }

                Console.WriteLine($"返回结果：{invokeResult}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool IsPropertyExist(dynamic data, string propertyname)
        {
            if (data is ExpandoObject)
                return ((IDictionary<string, object>)data).ContainsKey(propertyname);
            return data.GetType().GetProperty(propertyname) != null;
        }
    }
}
