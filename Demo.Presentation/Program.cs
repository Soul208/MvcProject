 using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services.AttachmentService;
using Demo.BusinessLogic.Services.DepartmentsServies;
using Demo.BusinessLogic.Services.Employees;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Moodels;
using Demo.DataAccess.Moodels.IdentityModel;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using Demo.Presentation.Helpers;
using Demo.Presentation.Settings;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllersWithViews(options =>
            {

                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            });

            // builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();

            });//better

            builder.Services.AddScoped<DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            #region employee-----------------


            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IunitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachmentService , AttachmentService>();



            #endregion

            builder.Services.AddIdentity<ApplicatonUser, IdentityRole>(Options =>
            {
                Options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            #endregion
            builder.Services.Configure<MailSettings>(
                builder.Configuration.GetSection("MailSettings")
                );

            builder.Services.Configure<SmsSettings>(builder.Configuration.GetSection("Twilio"));

            builder.Services.AddTransient<IMailService, MailService>();
            builder.Services.AddTransient<ISmsService, SmsService>();

            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

            }).AddGoogle(o =>
            {
                IConfiguration GoogleAuthSection = builder.Configuration.GetSection("Authentication:Google");
                o.ClientId = GoogleAuthSection["ClientId"];
                o.ClientSecret = GoogleAuthSection["ClientSecret"];

            });
            var app = builder.Build();

            #region Configure the HTTP request pipeline.

              if (!app.Environment.IsDevelopment())
              {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
              }

              app.UseHttpsRedirection();
              app.UseStaticFiles();

              app.UseRouting();

              app.UseAuthentication();
              app.UseAuthorization();

              app.MapControllerRoute(
              name: "default",
              pattern: "{controller=Account}/{action=Register}/{id?}");
            #endregion

            app.Run();
        }
    }
}
