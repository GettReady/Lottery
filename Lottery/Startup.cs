using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lottery.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lottery.Data.Repository;
using Lottery.Data.Interfaces;
using Hangfire;

namespace Lottery
{
    public class Startup
    {
        private IConfigurationRoot ConfString;

        public Startup(IWebHostEnvironment env)
        {
            ConfString = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LotteryDbContext>(options => options.UseSqlServer(ConfString.GetConnectionString("DefaultConnection")));
            services.AddTransient<IRaffle, RaffleRepository>();
            services.AddTransient<IUser, UserRepository>();
            services.AddTransient<IPrize, PrizeRepository>();
            services.AddTransient<IUserRaffle, UserRaffleRepository>();

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddMvc();
            services.AddControllersWithViews(mvcOtions =>
            {
                mvcOtions.EnableEndpointRouting = false;
            });

            services.AddHangfire(x => x.UseSqlServerStorage(ConfString.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRaffle raffle)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseSession();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
                {
                    routes.MapRoute(name: "default", template: "{controller=RaffleList}/{action=ShowRaffles}");
                }
            );

            using (var scope = app.ApplicationServices.CreateScope())
            {
                LotteryDbContext context = scope.ServiceProvider.GetRequiredService<LotteryDbContext>();
                //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                DbData.Initialize(context);
            }



            //var iRaffle = app.ApplicationServices.GetService<IRaffle>();


            //var iUserRaffle = app.ApplicationServices.GetService<IUserRaffle>();


            //Randomizer randomizer = new Randomizer(iRaffle/*, iUserRaffle*/);


            Randomizer randomizer = new Randomizer(raffle);

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate(() => randomizer.CheckRaffles(), "* * * * *");
        }
    }
}
