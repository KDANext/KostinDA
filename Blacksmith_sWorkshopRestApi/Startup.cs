using Blacksmith_sWorkshopBusinessLogic.BusinessLogics;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopDatebaseImplement.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blacksmith_sWorkshopRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MailLogic.MailConfig(new Blacksmith_sWorkshopBusinessLogic.HelperModels.MailConfig
            {
                SmtpClientHost = Configuration["MailService:SmtpClientHost"],
                SmtpClientPort = int.Parse(Configuration["MailService:SmtpClientPort"]),
                MailLogin = Configuration["MailService:MailLogin"],
                MailPassword = Configuration["MailService:MailPassword"],
            });

        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientLogic, ClientLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IProductLogic, ProductLogic>();
            services.AddTransient<MainLogic>();
            services.AddTransient<IMessageInfoLogic, MessageInfoLogic>();
            services.AddControllers().AddNewtonsoftJson();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
