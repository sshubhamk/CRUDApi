using CRUDApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CRUDApi {
    public class Startup {
        readonly string MyAllowSpecificOrigins = "MyAllowSpecificOrigins";
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddCors ();
            services.AddControllers ();
            services.AddDbContext<DB01Context>
                (options => options.UseSqlServer (Configuration.GetConnectionString ("DB01")));
            services.AddCors (options => {
                options.AddPolicy (MyAllowSpecificOrigins,
                    builder => {
                        builder.WithOrigins ("http://localhost:4200", "*").AllowAnyHeader ().AllowAnyMethod ().AllowAnyOrigin ();
                        builder.SetIsOriginAllowedToAllowWildcardSubdomains ();
                    });
            });
            services.AddSwaggerGen ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseCors (MyAllowSpecificOrigins);

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "CRUD API V1");
            });
        }
    }
}