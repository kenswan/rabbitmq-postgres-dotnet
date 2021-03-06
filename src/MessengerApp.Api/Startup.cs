using System;
using System.IO;
using System.Reflection;
using MessengerApp.Api.Models;
using MessengerApp.Api.Providers;
using MessengerApp.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace MessengerApp.Api
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
            services.AddControllers();

            services.AddDbContext<IStorageProvider, StorageProvider>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<RabbitMQConfig>(Configuration.GetSection("RabbitMQ"));

            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();

            services.AddHttpClient<IMessagingProvider, MessagingProviderHttp>(
                (serviceProvider, client) =>
                {
                    var rmq = serviceProvider.GetRequiredService<IOptions<RabbitMQConfig>>().Value;
                    client.BaseAddress = new Uri($"http://{rmq.UserName}:{rmq.Password}@{rmq.HostName}:{rmq.Port}");
                });

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Messaging API", Version = "V1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                config.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Message App API");
            });

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
