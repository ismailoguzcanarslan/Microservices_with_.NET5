// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes = { "catalog_fullpermission" } },
            new ApiResource("resource_photostock"){Scopes = { "photostock_fullpermission" } },
            new ApiResource("resource_basket"){Scopes = { "basket_fullpermission" } },
            new ApiResource("resource_discount"){Scopes = { "discount_fullpermission" } },
            new ApiResource("resource_order"){Scopes = { "order_fullpermission" } },
            new ApiResource("resource_payment"){Scopes = { "payment_fullpermission" } },
            new ApiResource("resource_gateway"){Scopes = { "gateway_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(){Name = "roles", DisplayName="Roles", Description = "User Roles", UserClaims = new []{ "role"} }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission", "Full Access To Catalog API"),
                new ApiScope("photostock_fullpermission", "Full Access To Photo Stock API"),
                new ApiScope("basket_fullpermission", "Full Access To Basket API"),
                new ApiScope("discount_fullpermission", "Full Access To Discount API"),
                new ApiScope("order_fullpermission", "Full Access To Order API"),
                new ApiScope("payment_fullpermission", "Full Access To Payment API"),
                new ApiScope("gateway_fullpermission", "Full Access To Gate Way API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName, "Full Access To Identity Server API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               new Client
               {
                   ClientName = "AspNetCoreMVC",
                   ClientId = "WebMvcClient",
                   ClientSecrets = {new Secret("secret".Sha256())},
                   AllowedGrantTypes = GrantTypes.ClientCredentials, //refresh token bulunmaz.
                   AllowedScopes = { "gateway_fullpermission", "catalog_fullpermission", "photostock_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
               },
               new Client
               {
                   ClientName = "AspNetCoreMVC",
                   ClientId = "WebMvcClientForUser",
                   AllowOfflineAccess = true,
                   ClientSecrets = {new Secret("secret".Sha256())},
                   AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                   AllowedScopes = { "gateway_fullpermission", "payment_fullpermission", "order_fullpermission", "basket_fullpermission", "discount_fullpermission", IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess, "roles", IdentityServerConstants.LocalApi.ScopeName},
                   AccessTokenLifetime = 1*60*60,
                   RefreshTokenExpiration = TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                   RefreshTokenUsage = TokenUsage.ReUse
               }
            };
    }
}