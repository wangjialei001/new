using Infrastructure.EncryptAndDecrypt.Des;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api.Filters
{
    public class ResourceFilterAttribute : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            Console.WriteLine("IAsyncResourceFilter开始");

            var controller = context.ActionDescriptor as ControllerActionDescriptor;
            var encryRequired = controller.MethodInfo.GetCustomAttributes(typeof(EncryRequiredAttribute), false).FirstOrDefault() as EncryRequiredAttribute;
            if (encryRequired != null && encryRequired.isRequired)
            {
                string key = "mvmN61X0";
                var requestBody = context.HttpContext.Request.Body;
                string apiBodySource = string.Empty, apiBody = string.Empty;
                string apiBodyReturnSource = string.Empty, apiBodyReturn = string.Empty;
                using (var newRequest = new MemoryStream())
                {
                    context.HttpContext.Request.Body = newRequest;
                    using (var reader = new StreamReader(requestBody))
                    {
                        apiBodySource = await reader.ReadToEndAsync();
                        if (string.IsNullOrWhiteSpace(apiBodySource))
                        {
                            string errorStr = JsonConvert.SerializeObject(new
                            {
                                code = 500,
                                msg = "数据为空！"
                            });
                            context.Result = new BadRequestObjectResult(DesUtil.DESEncrypt(errorStr, key, key));
                            return;
                        }
                        else
                        {
                            apiBody = DesUtil.DESDecrypt(apiBodySource, key, key);
                            using (var writer = new StreamWriter(newRequest))
                            {
                                await writer.WriteAsync(apiBody);
                                await writer.FlushAsync();
                                newRequest.Position = 0;
                                context.HttpContext.Request.Body = newRequest;
                                await next();
                                using (var newResponse = new MemoryStream())
                                {
                                    var responseBody = context.HttpContext.Response.Body;
                                    using (var readerReturn = new StreamReader(responseBody))
                                    {
                                        apiBodyReturnSource = await reader.ReadToEndAsync();
                                        if (!string.IsNullOrWhiteSpace(apiBodyReturnSource))
                                        {
                                            apiBodyReturn = DesUtil.DESEncrypt(apiBodyReturnSource, key, key);
                                            using (var writerReturn = new StreamWriter(newResponse))
                                            {
                                                await writer.WriteAsync(apiBodyReturn);
                                                await writer.FlushAsync();
                                                context.HttpContext.Response.Body = newResponse;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                await next();
            }
            Console.WriteLine("IAsyncResourceFilter结束");
        }
    }
}
