using IKEA.BLL.Profiles;
using IKEA.BLL.Services.AttachmentService;
using IKEA.BLL.Services.Classes;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Data.Contexts;
using IKEA.DAL.Models.IdentityModel;
using IKEA.DAL.Repositories.Classes;
using IKEA.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container.

            builder.Services.AddControllersWithViews( options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            //builder.Services.AddScoped<ApplicationDBContext>(); // 2.Register service in Dependancy Injection container
            builder.Services.AddDbContext<ApplicationDBContext>( options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });

            //builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            //builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            //builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<ApplicationDBContext>();
            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline (Middleware).

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
