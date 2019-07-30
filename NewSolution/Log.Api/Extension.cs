using Microsoft.AspNetCore.Http;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Api
{
    public static class Extension
    {
        /// <summary>
        /// 获取客户Ip
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
                ip = context.Connection.RemoteIpAddress.ToString();
            return ip;
        }
    }
    [LayoutRenderer("rq-ip")]
    public class RQIPLayoutRender : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append("测试RQIP");
        }
    }
    [LayoutRenderer("rq-ip1")]
    public class AspNetLayoutRendererBase1 : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var ip = HttpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
                ip = HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()+" "+HttpContextAccessor.HttpContext.Connection.RemotePort.ToString();
            builder.Append(ip);
        }
    }
}
