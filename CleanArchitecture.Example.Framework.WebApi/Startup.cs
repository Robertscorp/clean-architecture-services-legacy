using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.InterfaceAdapters.Controllers;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Infrastructure;
using CleanArchitecture.Services.Persistence;
using CleanArchitecture.Services.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            _ = services.AddAutoMapper(typeof(IPresenter<>).Assembly);
            _ = services.AddControllers();
            _ = services.AddScoped(typeof(IUseCaseElement<,>), typeof(RequestValidatorUseCaseElement<,>));
            _ = services.AddScoped(typeof(IUseCaseElement<,>), typeof(BusinessRuleValidatorUseCaseElement<,>));
            _ = services.AddScoped(typeof(IUseCaseElement<,>), typeof(InteractorUseCaseElement<,>));
            _ = services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            _ = services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchitecture.Example.Framework.WebApi", Version = "v1" }));

            _ = services.Scan(s => s.FromAssemblies(typeof(IUseCaseInvoker).Assembly, typeof(IPresenter<>).Assembly)
                                    .AddClasses(classes =>
                                        classes
                                            .Where(type => type.GetInterface(typeof(IUseCaseElement<,>).Name) == null)
                                            .Where(type => type != typeof(ValidationResult)))
                                    .AsImplementedInterfaces()
                                    .WithScopedLifetime());

            _ = services.Scan(s => s.FromAssemblies(typeof(GenderController).Assembly)
                                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Controller")))
                                    .AsSelf()
                                    .WithScopedLifetime());

            _ = services.AddScoped<IPersistenceContext, TempPersistenceContext>();
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

    public class TempPersistenceContext : IPersistenceContext
    {

        #region - - - - - - IPersistenceContext Implementation - - - - - -

        public Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
            => throw new NotImplementedException();

        public Task<TEntity> FindAsync<TEntity>(EntityID entityID, CancellationToken cancellationToken) where TEntity : class
            => throw new NotImplementedException();

        public Task<IQueryable<TEntity>> GetEntitiesAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class
            => throw new NotImplementedException();

        public Task<EntityID> GetEntityIDAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
            => throw new NotImplementedException();

        public Task RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
            => throw new NotImplementedException();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => throw new NotImplementedException();

        #endregion IPersistenceContext Implementation

    }

}
