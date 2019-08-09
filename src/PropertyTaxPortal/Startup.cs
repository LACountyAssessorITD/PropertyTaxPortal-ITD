
using System.Collections.Generic;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyTaxPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using PropertyTaxPortal.Services;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace PropertyTaxPortal
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var email = Configuration.GetSection("Email");
            services.Configure<Email>(email);
            services.AddDbContext<PTPContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("ptp_connect"));
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.ConfigureApplicationCookie(options => {

            //    options.Events = new CookieAuthenticationEvents
            //    {
            //        OnRedirectToLogin = ctx =>
            //        {
            //            var requestPath = ctx.Request.Path;
            //            if (requestPath.Value == "/admin")
            //            {
            //                ctx.Response.Redirect("/auth/signin");
            //            }
            //            else if (requestPath.Value == "/FAQs")
            //            {
            //                ctx.Response.Redirect("/auth/signin");
            //            }
            //            else if (requestPath.Value == "/News")
            //            {
            //                ctx.Response.Redirect("/auth/signin");
            //            }
            //            else if (requestPath.Value == "/Categories")
            //            {
            //                ctx.Response.Redirect("/auth/signin");
            //            }

            //            return Task.CompletedTask;
            //        }
            //    };

            //});

            



            //---------------------------CONFIGURE LOCALIZATION-----------------------------//

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("es"),
                        new CultureInfo("fr"),
                        new CultureInfo("zh"),
                        new CultureInfo("zh-TW"),
                        new CultureInfo("hy"),
                        new CultureInfo("ko"),
                        new CultureInfo("fa"),
                        new CultureInfo("ru"),
                        new CultureInfo("vi")
                    };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            
            services.AddSingleton<IUserService>(new AdminUserService());

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //    .AddCookie(options =>
            //    {
            //        options.LoginPath = "/auth/signin";
            //    });

            //---------------------------END CONFIGURE LOCALIZATION-----------------------------//
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseCookiePolicy();
            app.UseStaticFiles();
            // To configure external authentication, 
            // see: http://go.microsoft.com/fwlink/?LinkID=532715
            //app.UseAuthentication();


            app.Use(async (context, next) =>
            {
                // For external redirect
                /////////////////////////////////////////////////////////////////////////////////////////////
                var sHost = context.Request.Host.HasValue == true ? context.Request.Host.Value : "";
                sHost = sHost.ToLower();

                if (sHost.Contains("lacountypropertytax.com"))
                    context.Response.Redirect("https://www.propertytax.lacounty.gov");
                /////////////////////////////////////////////////////////////////////////////////////////////

                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });
            
            app.UseHttpsRedirection();
            app.UseRewriter(new RewriteOptions()
                .AddRedirectToWww()
                
            );
            //-----------------LOCALIZATION MIDDLEWARE-----------------//
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                        new CultureInfo("es"),
                        new CultureInfo("fr"),
                        new CultureInfo("zh"),
                        new CultureInfo("zh-TW"),
                        new CultureInfo("hy"),
                        new CultureInfo("ko"),
                        new CultureInfo("fa"),
                        new CultureInfo("ru"),
                        new CultureInfo("vi")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            


            //-----------------END LOCALIZATION MIDDLEWARE-----------------//


            //app.UseCsp(opts => opts
                
            //    .FontSources(s => s.Self()
            //    .CustomSources("https://ajax.aspnetcdn.com")
            //    .CustomSources("https://www.googletagmanager.com")
            //    .CustomSources("https://use.fontawesome.com")
            //    .CustomSources("https://cse.google.com")
            //    .CustomSources("https://apis.google.com")
            //    .CustomSources("https://www.googletagmanager.com"))

            //    .FormActions(s => s.Self())
            //    .FrameAncestors(s => s.Self())
            //    .ImageSources(s => s.Self())
            //    .StyleSources(s => s.Self()
            //    .CustomSources("https://ajax.aspnetcdn.com")
            //    .CustomSources("https://www.googletagmanager.com")
            //    .CustomSources("https://use.fontawesome.com")
            //    .CustomSources("https://cse.google.com")
            //    .CustomSources("https://apis.google.com")
            //    .CustomSources("https://www.googletagmanager.com"))
            //    .ScriptSources(s => s.Self()
            //    .UnsafeInline()
            //    .CustomSources("https://ajax.aspnetcdn.com")
            //    .CustomSources("https://fonts.googleapis.com")
            //    .CustomSources("https://use.fontawesome.com")
            //    .CustomSources("https://cse.google.com")
            //    .CustomSources("https://apis.google.com")
            //    .CustomSources("https://www.googletagmanager.com"))
            //);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
