using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterMontior
{
    using System.Text.Json;
    using Dapr.Client;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDaprClient();
            services.AddSingleton(new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, JsonSerializerOptions serializerOptions, DaprClient daprClient, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<Startup>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPost("/tweets", HandleTwitterMessage);
            });

            async Task HandleTwitterMessage(HttpContext context)
            {
                var tweet = await JsonSerializer.DeserializeAsync<Tweet>(context.Request.Body, serializerOptions);
                logger.LogInformation($"[binding.twitter] Tweet {tweet.id} from '{tweet.user}'");

                string monitoredTag = Configuration["MonitoredTag"];
                await daprClient.PublishEventAsync<TweetReceived>("messagebus", "mentions",
                    new TweetReceived { Tweet = tweet, Tag = monitoredTag });

                context.Response.StatusCode = 200;
            }
        }
    }
}
