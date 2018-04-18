using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeddingPlanner.Models;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace WeddingPlanner
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var Builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

            Configuration = Builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MySqlOptions>(Configuration.GetSection("DBInfo"));
            services.AddDbContext<WeddingPlannerContext>(options => options.UseMySQL(Configuration["DBInfo:ConnectionString"]));
            services.AddSession();
            services.AddMvc();        
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
        }
    }
}
