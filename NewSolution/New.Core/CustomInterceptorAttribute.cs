using AspectCore.DynamicProxy;
using New.Model;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace New.Core
{
    /// <summary>
    /// 自定义拦截器
    /// </summary>
    public class CustomInterceptorAttribute : AbstractInterceptorAttribute
    {
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                object[] objs = context.Parameters;
                if (objs != null && objs.Length > 0)
                {//修改参数值
                    for (int i = 0; i < objs.Length; i++)
                    {
                        var obj = objs[i];
                        if (obj.GetType() == typeof(Customer))
                        {
                            Customer customer = obj as Customer;
                            customer.Msg = "AOP修改值";
                            obj = customer;
                        }
                    }
                }
                Console.WriteLine("Befer service call");
                await next(context);
                var returnValue = context.ReturnValue;
                if(returnValue.GetType() == typeof(Customer))
                {//修改返回值
                    Customer returnCustomer = returnValue as Customer;
                    if (returnCustomer.Sex == "1")
                        returnCustomer.Sex = "男";
                    else
                        returnCustomer.Sex = "女";
                    context.ReturnValue = returnCustomer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception service call");
            }
            finally
            {
                Console.WriteLine("After service call");
            }
        }
    }
}
