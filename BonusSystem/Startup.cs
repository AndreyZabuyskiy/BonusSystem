using BonusSystem.Models.Db;
using BonusSystem.Models.Exceptions;
using BonusSystem.Models.Services;
using BonusSystem.Models.UseCases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BonusSystem
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
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                    .UseSqlServer(connection)
                    .Options;

            var db = new ApplicationContext(options);

            ClientService clientService = new ClientService(db);
            BonusCardService cardService = new BonusCardService(db);

            services.AddSingleton<ICreateClient>(provider => clientService);
            services.AddSingleton<IRemoveClient>(provider => clientService);
            services.AddSingleton<IEditClient>(provider => clientService);
            services.AddSingleton<ISaveClientDb>(provider => clientService);
            services.AddSingleton<ICreateBonusCard>(provider => cardService);
            services.AddSingleton<IDebit>(provider => cardService);
            services.AddSingleton<ICredit>(provider => cardService);

            services.AddControllersWithViews();
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

            app.UseRouting();

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
