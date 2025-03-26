using Demo.BusinessLogic.Services.DepartmentsServies;
using Demo.BusinessLogic.Services.Employees;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Moodels;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllersWithViews();
            // builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });//better

            builder.Services.AddScoped<DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            #region employee-----------------


            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IEmployeeService, EmployeeService>();

           #endregion


        #endregion

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

              app.UseAuthorization();

              app.MapControllerRoute(
              name: "default",
              pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            app.Run();
        }
    }
}
