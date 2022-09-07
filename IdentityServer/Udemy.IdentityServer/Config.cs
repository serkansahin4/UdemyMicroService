// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Udemy.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
               new IdentityResources.Email(),
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResource(){Name="roles", DisplayName="Roles", Description="Kullanıcı Rolleri",UserClaims=new[]{"role"}}
                   };
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission" } },
            new ApiResource("resource_photo_stock"){Scopes={"photostock_fullpermission" } },
            new ApiResource("resource_basket"){Scopes={"basket_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
               new ApiScope("catalog_fullpermission","Catalog Api İçin Ful Erişim"),
               new ApiScope("photostock_fullpermission","Photo Stock Api İçin Ful Erişim"),
               new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
               new ApiScope("basket_fullpermission","Sepete Erişim")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                   ClientId="WebMvcClient",
                   ClientName="Asp.NET Core Mvc",
                   ClientSecrets={new Secret("secret".Sha256()) },
                   AllowedGrantTypes={GrantType.ClientCredentials },
                   AllowedScopes={ IdentityServerConstants.LocalApi.ScopeName }
                },
                new Client
                {
                   ClientId="WebMvcClientForUser",
                   AllowOfflineAccess=true,
                   ClientName="Asp.NET Core Mvc",
                   ClientSecrets={new Secret("secret".Sha256()) },
                   AllowedGrantTypes=GrantTypes.ResourceOwnerPassword ,
                   AllowedScopes={ IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess,"roles"
                    ,"catalog_fullpermission","basket_fullpermission","photostock_fullpermission"}, AccessTokenLifetime=1*60*60,
                   RefreshTokenExpiration=TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                   RefreshTokenUsage=TokenUsage.ReUse
                }

            };
    }
}