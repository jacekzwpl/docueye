using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net;

namespace DocuEye.Infrastructure.Auth
{
    public static class OidcAuthHostingExtensions
    {
        public static WebApplicationBuilder AddOidcAuthentication(this WebApplicationBuilder builder, OidcSettings oidcSettings)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Events.OnRedirectToAccessDenied = context => {
                        context.Response.StatusCode = 403;
                        return Task.CompletedTask;
                    };
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = oidcSettings.Authority;

                    options.ClientId = oidcSettings.ClientId;
                    options.ClientSecret = oidcSettings.ClientSecret;
                    options.ResponseType = OpenIdConnectResponseType.Code;

                    options.Scope.Clear();
                    foreach (var scope in oidcSettings.Scopes)
                    {
                        options.Scope.Add(scope);
                    }

                    options.ClaimActions.MapJsonKey("roles", "realm_access_roles");
                    //options.ClaimActions.MapJsonKey("display_name", "display_name");
                    //options.ClaimActions.MapJsonKey("account_id", "account_id");

                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.RequireHttpsMetadata = builder.Environment.IsDevelopment() ? false : true;
                    options.SaveTokens = true;

                    options.UsePkce = true;

                    options.Events.OnRedirectToIdentityProvider = context =>
                    {

                        if (context.Request.Path.StartsWithSegments("/api"))
                        {
                            if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                            {
                                context.Response.StatusCode = 401;
                            }

                            context.HandleResponse();
                        }

                        return Task.CompletedTask;
                    };
                });

            return builder;
        }
    }
}
