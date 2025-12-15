using DocuEye.ChangeTracker.Api.Controllers;
using DocuEye.ChangeTracker.Application.EventHandlers;
using DocuEye.ChangeTracker.Persistence;
using DocuEye.DocsKeeper.Api.Controllers;
using DocuEye.DocsKeeper.Application.Commands.SaveSingleDecision;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Authentication.OIDC;
using DocuEye.Infrastructure.DataProtection;
using DocuEye.Infrastructure.Mediator;
using DocuEye.ModelKeeper.Api.Controllers;
using DocuEye.ModelKeeper.Application.Commands.SaveElements;
using DocuEye.ModelKeeper.Persistence;
using DocuEye.Persistence;
using DocuEye.ViewsKeeper.Api.Controllers;
using DocuEye.ViewsKeeper.Application.Commands.SaveViewsChanges;
using DocuEye.ViewsKeeper.Persistence;
using DocuEye.Web.Auth;
using DocuEye.WorkspaceImporter.Api;
using DocuEye.WorkspaceImporter.Api.Controllers;
using DocuEye.WorkspaceImporter.Application.Commands.StartImport;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.Workspaces.Api.Controllers;
using DocuEye.WorkspacesKeeper.Application;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveWorkspace;
using DocuEye.WorkspacesKeeper.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net;

namespace DocuEye.Web
{
    public static class HostingExtensions
    {

        public static IConfigurationBuilder UseConfiguration<T>(this IConfigurationBuilder builder, ILogger startupLogger, bool useEnvironment = true, string[]? environmentPrefixes = null) where T : class
        {
            
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                devEnvironmentVariable.ToLower() == "development";
            startupLogger.LogInformation("Adding appsettings.json as configuration source");
            builder.AddJsonFile("appsettings.json", optional: false);

            if (useEnvironment && (environmentPrefixes == null || environmentPrefixes.Length == 0))
            {
                startupLogger.LogInformation("Adding enviroment variables as configuration source");
                builder.AddEnvironmentVariables();
            }
            else if (useEnvironment && environmentPrefixes != null)
            {
                foreach (var prefix in environmentPrefixes)
                {
                    startupLogger.LogInformation("Adding enviroment variables with prefix {prefix} as configuration source", prefix);
                    builder.AddEnvironmentVariables(prefix);
                }
            }

            if (isDevelopment)
            {
                startupLogger.LogInformation("Adding user secrets as configuration source");
                builder.AddUserSecrets<T>();
            }

            return builder;
        }

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder, ILogger startupLogger)
        {

            startupLogger.LogInformation("Configure services");

            var connectionString = builder.Configuration["DocuEye:Database:ConnectionString"];
            var databaseName = builder.Configuration["DocuEye:Database:Name"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("There is no database connection configuration.");
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new Exception("There is no database name configuration.");
            }

            startupLogger.LogInformation("Register MongoDBContext");
            builder.Services.AddSingleton<MongoDBContext>((serviceProvider) =>
            {
                return ActivatorUtilities.CreateInstance<MongoDBContext>(serviceProvider, connectionString, databaseName);
            });

            builder.Services.AddSingleton<IChangeTrackerDBContext>(x => x.GetRequiredService<MongoDBContext>());
            builder.Services.AddSingleton<IViewsKeeperDBContext>(x => x.GetRequiredService<MongoDBContext>());
            builder.Services.AddSingleton<IDocsKeeperDBContext>(x => x.GetRequiredService<MongoDBContext>());
            builder.Services.AddSingleton<IModelKeeperDBContext>(x => x.GetRequiredService<MongoDBContext>());
            builder.Services.AddSingleton<IWorkspacesKeeperDBContext>(x => x.GetRequiredService<MongoDBContext>());
            builder.Services.AddSingleton<IWorkspaceImporterDBContext>(x => x.GetRequiredService<MongoDBContext>());
            builder.Services.AddSingleton<IDataProtectionDBContext>(x => x.GetRequiredService<MongoDBContext>());

            builder.Services.AddDataProtection().PersistKeysToMongoDB();

            ChangeTrackerBsonClassMapping.Register();
            ViewsKeeperBsonClassMapping.Register();
            WorkspacesKeeperBsonClassMapping.Register();
            DocsKeeperBsonClassMapping.Register();

            builder.Services.AddMemoryCache();

            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.TryAddSingleton<IUserAccessProvider, UserAccessProvider>();


            var oidcSettings = builder.Configuration.GetSection("DocuEye:OIDC").Get<OidcSettings?>();

            if(oidcSettings != null)
            {
                startupLogger.LogInformation("Add OIDC authentication");
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Events.OnRedirectToAccessDenied = context =>
                    {
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
                    foreach (var scope in oidcSettings.GetScopes())
                    {
                        options.Scope.Add(scope);
                    }

                    options.MapInboundClaims = false;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    if(oidcSettings.UsePKCE == 1)
                    {
                        options.UsePkce = true;
                    }

                    options.SaveTokens = true;

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

                })
                .AddScheme<AuthenticationSchemeOptions, BasicTokenAuthenticationHandler>("BasicTokenAuthentication", null);
            }
            else
            {
                startupLogger.LogInformation("OIDC settings are empty. No OIDC auth is added.");
                builder.Services.AddAuthentication("BasicTokenAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicTokenAuthenticationHandler>("BasicTokenAuthentication", null);

            }

            builder.Services.AddSingleton<IAuthorizationHandler, WorkspaceAccessRequirementHandler>();
            builder.Services.AddSingleton<IAuthorizationHandler, GeneralAccessRequirementHandler>();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Workspace", policy => {

                    //policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new WorkspaceAccessRequirement(oidcSettings == null ? false : true, oidcSettings?.ClaimType));
                });

                options.AddPolicy("General", policy => {

                    //policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new GeneralAccessRequirement(oidcSettings == null ? false : true));
                });
            });
            startupLogger.LogInformation("Register Mediator services");
            builder.Services.AddMediator(options =>
            {
                options.UseAssembly(typeof(StartImportCommandHandler).Assembly);
                options.UseAssembly(typeof(ElementChangedEventHandler).Assembly);
                options.UseAssembly(typeof(SaveViewsChangesCommandHandler).Assembly);
                options.UseAssembly(typeof(SaveSingleDecisionCommandHandler).Assembly);
                options.UseAssembly(typeof(SaveElementsCommandHandler).Assembly);
                options.UseAssembly(typeof(SaveWorkspaceCommandHandler).Assembly);
            });

            builder.Services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN-ADMIN";
            });

            builder.Services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
                .AddApplicationPart(typeof(WorkspacesImportController).Assembly)
                .AddApplicationPart(typeof(WorkspacesController).Assembly)
                .AddApplicationPart(typeof(ViewsController).Assembly)
                .AddApplicationPart(typeof(ElementsController).Assembly)
                .AddApplicationPart(typeof(DecisionsController).Assembly)
                .AddApplicationPart(typeof(ModelChangesController).Assembly)
                .AddControllersAsServices();

            
            builder.Services.ConfigureWorkspacesKeeperApplicationServices();

            startupLogger.LogInformation("Register Automapper configurations");

            builder.Services.AddSwaggerGen();

            return builder;
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            var oidcSettings = new OidcSettings();
            app.Configuration.GetSection("DocuEye:OIDC").Bind(oidcSettings);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            if(!string.IsNullOrEmpty(oidcSettings.Authority))
            {
                app.MapFallbackToFile("index.html").RequireAuthorization();
            }else
            {
                app.MapFallbackToFile("index.html");
            }
            

            return app;
        }
    }
}
