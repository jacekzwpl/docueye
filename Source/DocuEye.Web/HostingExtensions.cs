using AutoMapper;
using DocuEye.ChangeTracker.Application.EventHandlers;
using DocuEye.ChangeTracker.Persistence;
using DocuEye.DocsKeeper.Application.Commands.SaveImages;
using DocuEye.DocsKeeper.Application.Mappings;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Authentication.OIDC;
using DocuEye.Infrastructure.DataProtection;
using DocuEye.ModelKeeper.Application.Commands.SaveElements;
using DocuEye.ModelKeeper.Application.Mappings;
using DocuEye.ModelKeeper.Persistence;
using DocuEye.Persistence;
using DocuEye.ViewsKeeper.Application.Commands.SaveViewsChanges;
using DocuEye.ViewsKeeper.Application.Mappings;
using DocuEye.ViewsKeeper.Persistence;
using DocuEye.Web.Auth;
using DocuEye.WorkspaceImporter.Api;
using DocuEye.WorkspaceImporter.Application;
using DocuEye.WorkspaceImporter.Application.Commands.ImportWorkspace;
using DocuEye.WorkspaceImporter.Application.Mappings;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.WorkspacesKeeper.Application;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveWorkspace;
using DocuEye.WorkspacesKeeper.Application.Mappings;
using DocuEye.WorkspacesKeeper.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = oidcSettings.Authority;

                    options.ClientId = oidcSettings.ClientId;
                    options.ClientSecret = oidcSettings.ClientSecret;
                    options.ResponseType = "code";

                    options.Scope.Clear();
                    foreach (var scope in oidcSettings.GetScopes())
                    {
                        options.Scope.Add(scope);
                    }

                    options.MapInboundClaims = false;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.SaveTokens = true;
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
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Workspace", policy => {

                    //policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new WorkspaceAccessRequirement(oidcSettings == null ? false : true, oidcSettings?.ClaimType));
                });
            });

            startupLogger.LogInformation("Register MediatR services");
            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(ImportWorkspaceCommandHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(ElementChangedEventHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SaveViewsChangesCommandHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SaveImagesCommandHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SaveElementsCommandHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SaveWorkspaceCommandHandler).Assembly);
            });

            builder.Services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN-ADMIN";
            });

            builder.Services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                });//.AddApplicationPart(Assembly.GetAssembly(typeof(WorkspacesImportController))).AddControllersAsServices();

            builder.Services.ConfigureWorkspaceImporterApplicationServices();
            builder.Services.ConfigureWorkspacesKeeperApplicationServices();

            startupLogger.LogInformation("Register Automapper configurations");
            //Automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                
                mc.AddProfile(new ViewsKeeperMappingProfile());
                mc.AddProfile(new DocsKeeperMappingProfile());
                mc.AddProfile(new ModelKeeperMappingProfile());
                mc.AddProfile(new WorkspacesKeeperMappingProfile());
                mc.AddProfile(new WorkspaceImporterFromExplodedMappingProfile());
                mc.AddProfile(new WorkspaceImporterFromStructurizrMappingProfile());
                mc.AddProfile(new WorkspaceImporterMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

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

            if(oidcSettings != null)
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
