﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api
{
    public static class DecryptAndEncryptMiddlewareExtensions
    {
        public static IApplicationBuilder DecryptAndEncrypt(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DecryptAndEncryptMiddleware>();
        }
    }
    public class DecryptAndEncryptMiddleware
    {
        private readonly RequestDelegate _next;
        public DecryptAndEncryptMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            //context.Response.ContentType = "text/plain;utf-8";

            //await context.Response.WriteAsync("这个一个中间件");
        }
    }
}
