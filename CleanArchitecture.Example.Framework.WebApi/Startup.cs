using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Framework.Persistence;
using CleanArchitecture.Example.InterfaceAdapters.Controllers;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.FluentValidation;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Infrastructure;
using CleanArchitecture.Services.Persistence;
using CleanArchitecture.Services.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
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
            _ = services
                    .AddControllers(opts => opts.ModelBinderProviders.Insert(0, new EntityIDBinderProvider()))
                    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new EntityIDConverter()));

            _ = services.AddScoped(typeof(EntityIDValidator<>));
            _ = services.AddScoped(typeof(IUseCaseElement<,>), typeof(RequestValidatorUseCaseElement<,>));
            _ = services.AddScoped(typeof(IUseCaseElement<,>), typeof(BusinessRuleValidatorUseCaseElement<,>));
            _ = services.AddScoped(typeof(IUseCaseElement<,>), typeof(InteractorUseCaseElement<,>));
            _ = services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            _ = services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Example Api", Version = "v1" }));

            _ = services.Scan(s => s.FromAssemblies(
                                        typeof(IUseCaseInvoker).Assembly,
                                        typeof(IPresenter<>).Assembly,
                                        typeof(UserDto).Assembly)
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

            _ = services.AddSingleton<IPersistenceContext, PersistenceContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example Api v1"));
            }

            _ = app.UseHttpsRedirection();
            _ = app.UseRouting();
            _ = app.UseAuthorization();
            _ = app.UseEndpoints(endpoints => _ = endpoints.MapControllers());
        }

        #endregion Methods

    }


    public class EntityIDModelBinder : IModelBinder
    {

        #region - - - - - - IModelBinder Implementation - - - - - -

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var _EntityID = default(EntityID);
            var _Value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

            if (_Value.StartsWith(@""""))
                _EntityID = EntityIDProvider.GetEntityIDWithValue(_Value);

            else if (long.TryParse(_Value, out var _Long))
                _EntityID = EntityIDProvider.GetEntityIDWithValue(_Long);

            else if (Guid.TryParse(_Value, out var _Guid))
                _EntityID = EntityIDProvider.GetEntityIDWithValue(_Guid);

            bindingContext.Result = ModelBindingResult.Success(_EntityID);
            return Task.CompletedTask;
        }

        #endregion IModelBinder Implementation

    }

    public class EntityIDBinderProvider : IModelBinderProvider
    {

        #region - - - - - - Fields - - - - - -

        private readonly IModelBinder m_Binder = new EntityIDModelBinder();

        #endregion Fields

        #region - - - - - - IModelBinderProvider Implementation - - - - - -

        public IModelBinder GetBinder(ModelBinderProviderContext context)
            => context.Metadata.ModelType == typeof(EntityID)
                ? this.m_Binder
                : null;

        #endregion IModelBinderProvider Implementation

    }

}
