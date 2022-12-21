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
            //身分驗證
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.LoginPath = new PathString("/account/login"); //如果沒登錄就會到這頁
                opt.AccessDeniedPath = new PathString("/home/AccessDenied"); //如果沒有權限的話會到此頁面
                //opt.AccessDeniedPath = 
            }).AddFacebook(opt =>
            {
                opt.AppId = "2231432293671362";
                opt.AppSecret = "92fe729130daba268d63b61f098c7519";
            });
            services.AddControllersWithViews();
            //注入HttpContext抓使用者資料
            services.AddHttpContextAccessor();
            //EF CORE Context注入
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
                //指派連線字串
                conn.ConnectionString = Configuration.GetConnectionString("BuySite");
                return conn;
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddJsonOptions(x =>

   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;               //密碼要有數字
                options.Password.RequireLowercase = true;           //要有小寫英文字母
                options.Password.RequireNonAlphanumeric = false;    //不需要符號字元
                options.Password.RequireUppercase = true;           //要有大寫英文字母
                options.Password.RequiredLength = 6;                //密碼至少要6個字元長
                options.Password.RequiredUniqueChars = 1;           //至少要有1個字元不一樣

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //5分鐘沒有動靜就自動鎖住定網站，預設5分鐘
                options.Lockout.MaxFailedAccessAttempts = 5; //三次密碼誤就鎖定網站, 預設5次
                options.Lockout.AllowedForNewUsers = true; //新增的使用者也會被鎖定，就是犯規沒有新人優待

                // User settings.
                //options.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; //郵箱不能重覆使用
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login"; //登入頁
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";//登出Action
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
            //設定Session
            app.UseSession();
            //app.Use(async (context, next) =>
            //{
            //    context.Session.SetString("SessionKey", "SessoinValue");
            //    await next.Invoke();
            //});
            app.UseRouting();
            
            //身分驗證
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
