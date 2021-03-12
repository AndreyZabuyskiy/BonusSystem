using BonusSystem.Models.Db;
using BonusSystem.Models.Services;
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

            ICreateClient createClient = new CreateClientService(db);
            IRemoveClient removeClient = new RemoveClientService(db);
            IEditClient editClient = new EditClientService(db);
            IDebit debit = new DebitService(db);
            ICreditFunds creditFunds = new CreditFundsService(db);

            services.AddSingleton<ICreateClient>(provider => createClient);
            services.AddSingleton<IRemoveClient>(provider => removeClient);
            services.AddSingleton<IEditClient>(provider => editClient);
            services.AddSingleton<IDebit>(provider => debit);
            services.AddSingleton<ICreditFunds>(provider => creditFunds);

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
