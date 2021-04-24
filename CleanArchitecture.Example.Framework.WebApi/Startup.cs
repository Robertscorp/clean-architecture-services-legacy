using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CleanArchitecture.Example.Framework.WebApi
{

    public class Startup
    {

        #region - - - - - - Constructors - - - - - -

        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public IConfiguration Configuration { get; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddControllers();
            _ = services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchitecture.Example.Framework.WebApi", Version = "v1" }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture.Example.Framework.WebApi v1"));
            }

            _ = app.UseHttpsRedirection();
            _ = app.UseRouting();
            _ = app.UseAuthorization();
            _ = app.UseEndpoints(endpoints => _ = endpoints.MapControllers());
        }

        #endregion Methods

    }

}
