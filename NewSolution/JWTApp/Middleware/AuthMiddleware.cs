using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTApp.Middleware
{
    public static class AuthService
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options=> {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ClockSkew = TimeSpan.FromSeconds(30),
                        AudienceValidator = (m, n, z) =>
                        {
                            return true;// m != null && m.FirstOrDefault().Equals(Const.ValidAudience);
                        },
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = configuration["Domain"],//Audience
                        ValidIssuer = configuration["Domain"],//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey"]))//拿到SecurityKey
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            //Token expired
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            
            return services;
        } 
    }
    public class AuthModel
    {
        public string UserName { get; set; }
        public string Pwd { get; set; }
    }
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration configuration;
        public AuthMiddleware(RequestDelegate _next, IConfiguration configuration)
        {
            this._next = _next;
            this.configuration = configuration;
        }
        private SortedDictionary<string, object> data;
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.StartsWith("/api/auth") && string.Equals(context.Request.Method, "post", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Response.ContentType = "application/json";
                data = new SortedDictionary<string, object>();
                Stream stream = context.Request.Body;
                string authRequestStr = await new StreamReader(stream).ReadToEndAsync();
                data.Add("request.Body", authRequestStr);
                if (string.IsNullOrWhiteSpace(authRequestStr))
                {
                    await context.Response.WriteAsync("{\"Success\":false,\"Msg\":\"参数为空\"}");
                    return;
                }

                var authInfo = JsonConvert.DeserializeObject<AuthModel>(authRequestStr);
                Console.WriteLine($"{authInfo.UserName}、{authInfo.Pwd}");
                if (!((string.Equals("wjl", authInfo.UserName) && string.Equals("123", authInfo.Pwd))
                    || (string.Equals("jack", authInfo.UserName) && string.Equals("123", authInfo.Pwd))))
                {
                    await context.Response.WriteAsync("{\"Success\":false,\"Msg\":\"用户名或者密码错误\"}");
                    return;
                }
                int expMinutes = int.Parse(configuration["Expires"]);
                DateTime expTime = DateTime.Now.AddMinutes(expMinutes);
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(expTime).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Name, authInfo.UserName)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: configuration["Domain"],//指定 Token 签发者，也就是这个签发服务器的名称
                    audience: configuration["Domain"],//指定 Token 接收者
                    claims: claims,//生成token信息
                    expires: expTime,//失效时间
                    signingCredentials: creds);
                var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
                await context.Response.WriteAsync("{\"Success\":true,\"Token\":\"" + tokenStr + "\"}");
                return;
            }
            else if (context.Request.Path.Value.StartsWith("/api/getUserByToken"))
            {
                if (context.Request.Headers.ContainsKey("Authorization"))
                {
                    var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Replace(" ", "");


                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token);
                    var tokenS = handler.ReadToken(token) as JwtSecurityToken;

                    var name = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
                    Console.WriteLine(name);
                }
            }
            await _next(context);
        }
    }
    public static class AuthMiddlewareExtension
    {
        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            //添加JWT验证
            app.UseAuthentication();
            return app.UseMiddleware<AuthMiddleware>();
        }
    }
}
