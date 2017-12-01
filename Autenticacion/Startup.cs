using Autenticacion.Data;
using Autenticacion.Quickstart;
using Autenticacion.Repositories;
using Autenticacion.Services;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;

namespace Autenticacion
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
            services.AddDbContext<AutenticacionContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AutenticacionConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
           .AddEntityFrameworkStores<AutenticacionContext>()
            .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddScoped<UserProfileRepository>();
            services.AddScoped<UserProviderRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<UserService>();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(TestIdentityResources.GetIdentityResources())
                .AddInMemoryApiResources(TestApiResources.GetApiResources())
                .AddInMemoryClients(TestClients.GetClients())
                .AddProfileService<PofileService>();

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com";
                    options.ClientSecret = "wdfPY6t8H8cecgjlxud__4Gh";
                })
                .AddOpenIdConnect("demoidsrv", "IdentityServer", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                    options.Authority = "https://demo.identityserver.io/";
                    options.ClientId = "implicit";
                    options.ResponseType = "id_token";
                    options.SaveTokens = true;
                    options.CallbackPath = new PathString("/signin-idsrv");
                    options.SignedOutCallbackPath = new PathString("/signout-callback-idsrv");
                    options.RemoteSignOutPath = new PathString("/signout-idsrv");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseIdentityServer();



            app.UseMvcWithDefaultRoute();
        }
    }
}
