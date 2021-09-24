using Autofac;
using FluentValidation.AspNetCore;
using GalacticEmpire.Api.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.Features.Event.Queries;
using GalacticEmpire.Application.Mapper;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Domain.Models.UserModel.Base;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;

namespace GalacticEmpire.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowAnyHeader());
            });


            services.AddDbContext<GalacticEmpireDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<GalacticEmpireDbContext>()
                .AddDefaultTokenProviders();
            
            services.Configure<IdentityOptions>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 8;

                opts.SignIn.RequireConfirmedEmail = true;
            });

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddHttpContextAccessor();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddIdentityServer(options =>
            {
                options.UserInteraction = new UserInteractionOptions()
                {
                    LogoutUrl = "/logout",
                    LoginUrl = "/login",

                    LoginReturnUrlParameter = "returnUrl"
                };
                options.Authentication.CookieAuthenticationScheme = IdentityConstants.ApplicationScheme;
            })
                .AddInMemoryClients(IdentityConfiguration.Clients)
                .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
                .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                .AddAspNetIdentity<User>()
                .AddDeveloperSigningCredential();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetValue<string>("Authentication:Authority");
                    options.Audience = Configuration.GetValue<string>("Authentication:Audience");
                    options.RequireHttpsMetadata = false;
                }
                );

            services.AddAuthorization(options =>
            {
                // There is no role based authorization in the app, as all users are in the same role
                // But there is a scope based authorization for the clients.
                // The client app can only execute the request if it has the required scope
                options.AddPolicy("api-openid", policy => policy.RequireAuthenticatedUser()
                    .RequireClaim("scope", "GalacticEmpireApi.all")
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.DefaultPolicy = options.GetPolicy("api-openid");
            });

            services.AddSwaggerDocument(config =>
            {
                config.DocumentProcessors.Add(new SecurityDefinitionAppender("Basic",
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.Basic,
                        Name = "Authorization",
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        In = OpenApiSecurityApiKeyLocation.Header
                    }));
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "GalacticEmpire API";
                    document.Info.Description = "BME BSc Szakdolgozat - Surmann Roland";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Surmann Roland",
                        Email = "surmannroland@gmail.com",
                        Url = "https://www.linkedin.com/in/rolandsurmann/"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });

            services.AddMediatR(typeof(GetAllEventsQuery));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            services.AddControllers().AddFluentValidation();

            services.AddRazorPages();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("GalacticEmpire.Application"))
                .Where(x => x.Name.EndsWith("Validator"))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseOpenApi();
            app.UseSwaggerUi3(options =>
            {
                options.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = "swagger",
                    ClientSecret = null,
                    AppName = "",
                    UsePkceWithAuthorizationCodeGrant = true
                };
            });

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
