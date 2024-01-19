using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using SmartShop.UI.Models;
using System.Security.Claims;

namespace SmartShop.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddCookiePolicy(opts =>
            {

            });

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddGoogle(googleOptions =>
                    {
                        googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                        googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                        
                        // Map the external picture claim to the internally used image claim
                        googleOptions.ClaimActions.MapJsonKey("image", "picture");
                        googleOptions.Scope.Add("profile");
                    });

            // Add a factory for our SmartShopAPI client:
            builder.Services.AddHttpClient("SmartShopClient", client =>
            {
                var config = configuration.GetSection("SmartShopApi").Get<SmartShopApiConfig>();
                client.BaseAddress = new Uri(config.BaseUri);
                client.DefaultRequestHeaders.Add(config.ApiKey, config.ApiSecret);
            });


            var app = builder.Build();

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}