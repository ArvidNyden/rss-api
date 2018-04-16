using ArvidNyden.Api.Rss.Application;
using ArvidNyden.Api.Rss.Application.Interfaces;
using ArvidNyden.Api.Rss.Infrastrucutre;
using ArvidNyden.Api.Rss.Infrastrucutre.Configuration;
using ArvidNyden.Api.Rss.Infrastrucutre.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace ArvidNyden.Api.Rss
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
            services.AddOptions();
            services.AddMvc();
            services.Configure<KeyVaultConfiguration>(Configuration);
            services.Configure<RssTableConfiguration>(Configuration);
            services.Configure<RssImageConfiguration>(Configuration);

            services.AddSingleton<IStorageSecurityService>(sp =>
            {
                var config = sp.GetService<IOptions<KeyVaultConfiguration>>();
                return new StorageSecurityService(config.Value);
            });

            services.AddTransient<IRssContentService>(sp =>
            {
                var config = sp.GetService<IOptions<RssTableConfiguration>>();
                var tableClient = sp.GetService<IStorageSecurityService>().GetTableClient();
                return new RssTableService(tableClient, config.Value);
            });

            services.AddTransient<IRssImageService>(sp =>
            {
                var config = sp.GetService<IOptions<RssImageConfiguration>>();
                var blobClient = sp.GetService<IStorageSecurityService>().GetBlobClient();
                return new RssImageService(blobClient, config.Value);
            });

            services.AddTransient<IRssFeedService, RssFeedService>();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "Rss API", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Arvid Nyden Rss Api"));

            app.UseMvc();
        }
    }
}
