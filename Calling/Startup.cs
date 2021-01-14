// © Microsoft Corporation. All rights reserved.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Calling
{
    public class Startup
    {
        private const string AllowAnyOrigin = nameof(AllowAnyOrigin);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Allow CORS as our client may be hosted on a different domain.
            services.AddCors(options =>
            {
                options.AddPolicy(AllowAnyOrigin,
                    builder =>
                    {
                        builder.SetIsOriginAllowed(origin => true);
                        builder.AllowCredentials();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    }
                );
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors(AllowAnyOrigin);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
