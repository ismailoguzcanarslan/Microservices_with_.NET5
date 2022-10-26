﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes = { "catalog_fullpermission" } },
            new ApiResource("resource_photostock"){Scopes = { "photostock_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
            
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission", "Full Access To Catalog API"),
                new ApiScope("photostock_fullpermission", "Full Access To Photo Stock API"),
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
                   AllowedGrantTypes = GrantTypes.ClientCredentials,
                   AllowedScopes = { "catalog_fullpermission", "photostock_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
               }
            };
    }
}