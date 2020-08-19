// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AuthService.Models.DBTest;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthService
{
    public static class Config
    {
        public static Claim[] GetClaim(User user)
        {
            return new Claim[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(JwtClaimTypes.Name,user.Name),
                //new Claim(JwtClaimTypes.BirthDate, user.Birthday!=null?((DateTime)user.Birthday).ToString("yyyy-MM-dd"):string.Empty),
                //new Claim(JwtClaimTypes.Gender,user.Sex==true?"男":"女"),
                //new Claim(JwtClaimTypes.Email, user.Email),
                //new Claim(JwtClaimTypes.Role,"?")
            };
        }
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                         new IdentityResources.Profile(),
                         new IdentityResources.OpenId(),
                         //new IdentityResources.Email(),
                         //new IdentityResources.Address(),
                         //new IdentityResources.Phone()
                   };

        public static IEnumerable<ApiResource> GetApiResources => new List<ApiResource>
        {
            new ApiResource {
                Scopes=new List<string>{ "api1"}//!!!重要
            }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1","My Api")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client{
                    ClientId="AuthTestApi",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,//客户端授权模式
                    ClientSecrets={
                        new Secret("AuthTestApi".Sha256())
                    },
                     AllowedScopes = {    "api1",                   //客户端允许访问个人信息资源的范围
                         //IdentityServerConstants.StandardScopes.Profile,
                         //IdentityServerConstants.StandardScopes.OpenId,
                         //IdentityServerConstants.StandardScopes.Email,
                         //IdentityServerConstants.StandardScopes.Address,
                         //IdentityServerConstants.StandardScopes.Phone
                     }
                },

                new Client{
                    ClientId="AuthTestApi1",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime=5,//设置token失效时间，默认单位秒，比如说设置为5秒，但是5秒后并不会失效，系统隐含一个时间偏移，当通过AccessToken访问api时，由于网络阻塞，超过5秒才到达服务器，所以Token在默认失效时间后，存在偏移量，默认5分钟，也就是系统实际会在5分钟+5秒后才会失效，JwtValidationClockSkew可以设置偏移量值
                    ClientSecrets={
                        new Secret("AuthTestApi1".Sha256())
                    },
                    // scopes that client has access to
                     AllowedScopes = {       "api1",                //客户端允许访问个人信息资源的范围
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.OpenId,
                         //IdentityServerConstants.StandardScopes.Email,
                         //IdentityServerConstants.StandardScopes.Address,
                         //IdentityServerConstants.StandardScopes.Phone
                     },
                     //RedirectUris=new List<string>{"http://localhost/view/" },
                     //PostLogoutRedirectUris=new List<string>{"http://localhost/view/" },
                     //AllowedCorsOrigins=new List<string>{ "http://localhost/view/" },
                     AllowOfflineAccess=true,
                     AllowAccessTokensViaBrowser=true,
                     AlwaysIncludeUserClaimsInIdToken=true
                },
                //new Client{
                //    ClientId="AuthTestApi2",
                //    AllowedGrantTypes=GrantTypes.Implicit,
                //    ClientSecrets={
                //        new Secret("AuthTestApi2".Sha256())
                //    },
                //    // scopes that client has access to
                //     AllowedScopes = {       "api1",                //客户端允许访问个人信息资源的范围
                //         IdentityServerConstants.StandardScopes.Profile,
                //         IdentityServerConstants.StandardScopes.OpenId,
                //         //IdentityServerConstants.StandardScopes.Email,
                //         //IdentityServerConstants.StandardScopes.Address,
                //         //IdentityServerConstants.StandardScopes.Phone
                //     }
                //}
                new Client{
                    ClientId="AuthTestApi3",
                    ClientName="AuthTestApi3 Client",
                    AllowedGrantTypes=GrantTypes.Code,//客户端获取code，根据code请求token
                    //是否允许申请 Refresh Tokens
                   //参考地址 https://identityserver4.readthedocs.io/en/latest/topics/refresh_tokens.html
                    AllowOfflineAccess=true,//
                    ClientSecrets={
                        new Secret("AuthTestApi3".Sha256())
                    },
                    // 登陆以后 我们重定向的地址(客户端地址)，
                    // {客户端地址}/signin-oidc是系统默认的不用改，也可以改，这里就用默认的
                    RedirectUris={ "http://localhost/view"},
                    //注销重定向的url
                    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },
                    //将用户claims 写人到IdToken,客户端可以直接访问
                    AlwaysIncludeUserClaimsInIdToken=true,
                    //AllowedCorsOrigins=new List<string>{ "http://localhost/view/" },
                     AllowedScopes = {
                        "api1",                   //客户端允许访问个人信息资源的范围
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.OpenId,
                         //IdentityServerConstants.StandardScopes.Email,
                         //IdentityServerConstants.StandardScopes.Address,
                         //IdentityServerConstants.StandardScopes.Phone
                     },
                     RequirePkce=false,//代码质询
                     AllowAccessTokensViaBrowser=true
                },
            };
    }
}
