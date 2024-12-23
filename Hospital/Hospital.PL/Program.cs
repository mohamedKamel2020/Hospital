using Hospital.BLL.Interfaces;
using Hospital.BLL.Repository;
using Hospital.DAL.Context;
using Hospital.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<HospitalDbContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IGenericRepository<Patient>, PatientRepository>();
            builder.Services.AddScoped<IGenericRepository<Depatment>, DepartmentRepository>();

            builder.Services.AddScoped<IGenericRepository<Ward>, WardRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();//can return files in wwwroot css, imgs

            app.UseRouting();//match rout => url+url Path

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Department}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
