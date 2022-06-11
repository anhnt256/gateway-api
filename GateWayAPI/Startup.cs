using GateWayAPI.Areas.Admin.IRepository;
using GateWayAPI.Areas.Admin.ModelHub;
using GateWayAPI.Areas.Admin.Repository;
using GateWayAPI.IRepository;
using GateWayAPI.IRepository.CSM;
using GateWayAPI.IRepository.GateWay;
using GateWayAPI.Models.ClientHub;
using GateWayAPI.Models.GateWay.General.AppSettings;
using GateWayAPI.Repository;
using GateWayAPI.Repository.CSM;
using GateWayAPI.Repository.GateWay;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateWayAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:4000", "http://192.168.1.250:4000", "http://192.168.1.250:5000", "http://localhost:5000")
                    .AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders("Content-Disposition");
                });
            });

            services.AddSignalR(e => {
                e.MaximumReceiveMessageSize = 102400000;
            });

            services.AddHangfire(config => config.UseSqlServerStorage(Configuration.GetConnectionString("GateWay")));
            services.AddHangfireServer();

            services.AddControllers().AddControllersAsServices();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("GeneralSetting");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });


            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddTransient<IUserRepository>(x => new UserRepository(Configuration.GetConnectionString("CSM")));
            services.AddTransient<IPaymentRepository>(x => new PaymentRepository(Configuration.GetConnectionString("CSM")));
            services.AddTransient<IServiceRepository>(x => new ServiceRepository(Configuration.GetConnectionString("CSM")));
            services.AddSingleton<IComputerRepository>(x => new ComputerRepository(Configuration.GetConnectionString("CSM")));

            services.AddSingleton<IAccountRepository>(x => new AccountRepository(Configuration.GetConnectionString("GateWay")));
            services.AddSingleton<IGameRepository>(x => new GameRepository(Configuration.GetConnectionString("GateWay")));
            services.AddSingleton<IStaffRepository>(x => new StaffRepository(Configuration.GetConnectionString("GateWay")));
            services.AddSingleton<IProductRepository>(x => new ProductRepository(Configuration.GetConnectionString("GateWay")));
            services.AddSingleton<IOrderRepository>(x => new OrderRepository(Configuration.GetConnectionString("GateWay")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                 name: "Admin",
                 areaName: "Admin",
                 pattern: "Admin/{controller=Home}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<AdminHub>("/adminHub");// Register Hub class
                endpoints.MapHub<ClientHub>("/clientHub");
            });
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
