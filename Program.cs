using CinemaHub.Data;
using CinemaHub.Data.Cart;
using CinemaHub.Data.Services;
using CinemaHub.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // DBContext Configuration
            builder.Services.AddDbContext<AppDbContext> (options =>
            {
                options.UseSqlServer (builder.Configuration.GetConnectionString ("DefaultConnection"));
            });
            // Service Configuration
            builder.Services.AddScoped<IActorService, ActorService> ();
            builder.Services.AddScoped<IProducerService, ProducerService> ();
            builder.Services.AddScoped<ICinemaService, CinemaService> ();
            builder.Services.AddScoped<IMovieService, MovieService> ();
            builder.Services.AddScoped<IOrdersService,ordersService > ();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor> ();
            builder.Services.AddScoped (sc => ShoppingCart.GetShoppingCart (sc));
            //authentication and authorization
            builder.Services.AddIdentity<ApplicationUser, IdentityRole> ().AddEntityFrameworkStores<AppDbContext> ();
            builder.Services.AddMemoryCache ();
            builder.Services.AddSession ();
            builder.Services.AddAuthentication (options =>
             {
                 options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

             });
                var app = builder.Build();
            //seed database
            AppDbInitializer.Seed (app);
            AppDbInitializer.SeedUsersAndRolesAsync (app).Wait ();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession ();
            app.UseAuthentication ();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
