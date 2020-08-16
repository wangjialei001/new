// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthService
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                         new IdentityResources.Profile(),
                         new IdentityResources.OpenId(),
                         //new IdentityResources.Email(),
                         //new IdentityResources.Address(),
                         //new IdentityResources.Phone()
                   };

        public static IEnumerable<ApiResource> GetApiResources => new List<ApiResource> { new ApiResource("api1","My Api")};

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
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
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.OpenId,
                         //IdentityServerConstants.StandardScopes.Email,
                         //IdentityServerConstants.StandardScopes.Address,
                         //IdentityServerConstants.StandardScopes.Phone
                     }
                },

                new Client{
                    ClientId="AuthTestApi1",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AccessTokenLifetime=5,
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
                     }
                }
            };
    }
}
