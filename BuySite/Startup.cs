using BuySite.Models.Entity;
using BuySite.Service.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Common.Helper;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace BuySite
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
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            //��������
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.LoginPath = new PathString("/account/login"); //�p�G�S�n���N�|��o��
                opt.AccessDeniedPath = new PathString("/home/AccessDenied"); //�p�G�S���v�����ܷ|�즹����
                //opt.AccessDeniedPath = 
            }).AddFacebook(opt =>
            {
                opt.AppId = "2231432293671362";
                opt.AppSecret = "92fe729130daba268d63b61f098c7519";
            });
            services.AddControllersWithViews();
            //�`�JHttpContext��ϥΪ̸��
            services.AddHttpContextAccessor();
            //EF CORE Context�`�J
            //services.AddDbContext<BuySiteDBContext>(option =>
            //    option.UseSqlServer(Configuration.GetConnectionString("BuySite")));
            services.AddDbContext<Models.Entity.BuySiteDBContext>((builider) =>
                { builider.UseSqlServer(this.Configuration.GetConnectionString("BuySite")); });
            //services.AddIdentity<IdentityUser, IdentityRole>()
            //.AddEntityFrameworkStores<BuySiteDBContext>();

            services.AddScoped<ProductRepository>();
            services.AddScoped<AppInfo>();
            services.AddDefaultIdentity<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<BuySiteDBContext>();

            services.AddScoped<IDbConnection, SqlConnection>(serviceProvider => {
                SqlConnection conn = new SqlConnection();
                //�����s�u�r��
                conn.ConnectionString = Configuration.GetConnectionString("BuySite");
                return conn;
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddJsonOptions(x =>

   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;               //�K�X�n���Ʀr
                options.Password.RequireLowercase = true;           //�n���p�g�^��r��
                options.Password.RequireNonAlphanumeric = false;    //���ݭn�Ÿ��r��
                options.Password.RequireUppercase = true;           //�n���j�g�^��r��
                options.Password.RequiredLength = 6;                //�K�X�ܤ֭n6�Ӧr����
                options.Password.RequiredUniqueChars = 1;           //�ܤ֭n��1�Ӧr�����@��

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //5�����S�����R�N�۰����w�����A�w�]5����
                options.Lockout.MaxFailedAccessAttempts = 5; //�T���K�X�~�N��w����, �w�]5��
                options.Lockout.AllowedForNewUsers = true; //�s�W���ϥΪ̤]�|�Q��w�A�N�O�ǳW�S���s�H�u��

                // User settings.
                //options.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; //�l�c���୫�Шϥ�
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login"; //�n�J��
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";//�n�XAction
                options.SlidingExpiration = true;
            });

            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //}).AddCookie(opt =>
            //{
            //    opt.LoginPath = "/account/login";
            //}).AddFacebook(opt =>
            //{
            //    opt.AppId = "2231432293671362";
            //    opt.AppSecret = "92fe729130daba268d63b61f098c7519";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //�]�wSession
            app.UseSession();
            //app.Use(async (context, next) =>
            //{
            //    context.Session.SetString("SessionKey", "SessoinValue");
            //    await next.Invoke();
            //});
            app.UseRouting();
            
            //��������
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
