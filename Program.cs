using Microsoft.EntityFrameworkCore;
using TVProject.Data;
using TVProject.Data.DataBase;
using TVProject.Data.Interfaces;
using TVProject.Data.Repository;
using TVProject.Data.Seed;
using TVProject.Data.Services.ActorServices;
using TVProject.Data.Services.CinemaServices;
using TVProject.Data.Services.MovieServices;
using TVProject.Data.Services.ProducerService;
using Microsoft.AspNetCore.Identity;
using TVProject.Data.Services.CartServices;
using TVProject.Data.Services.OrderServices;
using Microsoft.Exchange.WebServices.Data;
using TVProject.Data.Services.ElasticSearch;
using Nest;
using Hangfire;
using TVProject.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace TVProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            var redisConnectionString = builder.Configuration.GetConnectionString("Redis");

            if (string.IsNullOrEmpty(redisConnectionString))
            {
                throw new ArgumentNullException("Redis connection string cannot be null or empty.");
            }

            // Add Redis caching with the Redis connection string
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IActorService, ActorService>();
            builder.Services.AddScoped<IProducerService, ProducerService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<ICinemaService, CinemaService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IOrderService,  OrderService>();
            builder.Services.AddSingleton<ElasticSearchService>();
            builder.Services.AddScoped<SubscriptionChecker>();
            builder.Services.AddLogging(configure => configure.AddConsole());
            builder.Services.AddSingleton<IEmailSender, EmailService>();


            //using (var scope = builder.Services.BuildServiceProvider())
            //{
            //    scope.GetRequiredService<IMovieService>().syncAllMovies().Wait();
            //}

            builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString(name: "DefaultConnection")));
            builder.Services.AddHangfireServer();


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

            app.UseAuthorization();
            app.UseHangfireDashboard(pathMatch:"/dashboard");
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate<SubscriptionChecker>(
            "check-subscription-expiration",checker => checker.CheckSubscriptionExpiration(),Cron.Daily);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            AppDbInitializer.Seed(app);
            app.UseEndpoints(endpoints => endpoints.MapRazorPages());

            app.Run();
        }
    }
}
