using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Example.InterfaceAdapters.Controllers;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.EFCore;
using CleanArchitecture.Services.Extended.FluentValidation;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Extended.Queryable;
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
            _ = services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new EntityIDConverter()));
            _ = services.AddScoped(typeof(IUseCaseElement<,>), typeof(RequestValidatorUseCaseElement<,>));
            _ = services.AddScoped(typeof(IUseCaseElement<,>), typeof(BusinessRuleValidatorUseCaseElement<,>));
            _ = services.AddScoped(typeof(IUseCaseElement<,>), typeof(InteractorUseCaseElement<,>));
            _ = services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            _ = services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchitecture.Example.Framework.WebApi", Version = "v1" }));

            _ = services.Scan(s => s.FromAssemblies(
                                        typeof(IUseCaseInvoker).Assembly,
                                        typeof(IPresenter<>).Assembly,
                                        typeof(UserDto).Assembly)
                                    .AddClasses(classes =>
                                        classes
                                            .Where(type => type.GetInterface(typeof(IUseCaseElement<,>).Name) == null)
                                            .Where(type => type != typeof(PersistenceContext))
                                            .Where(type => type != typeof(QueryableUnion<>))
                                            .Where(type => type != typeof(ValidationResult))
                                            .Where(type => !type.IsAssignableTo(typeof(IQueryable))))
                                    .AsImplementedInterfaces()
                                    .WithScopedLifetime());

            _ = services.Scan(s => s.FromAssemblies(typeof(GenderController).Assembly)
                                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Controller")))
                                    .AsSelf()
                                    .WithScopedLifetime());

            _ = services.AddScoped<IPersistenceContext, TempPersistenceContext>();


            var _PersonQuery1 = new[]
            {
                new Person { EmailAddress = "I" },
                new Person { EmailAddress = "C" },
                new Person { EmailAddress = "A" },
                new Person { EmailAddress = "G" },
                new Person { EmailAddress = "Y" }
            }.AsQueryable();
            var _PersonQuery2 = new[]
            {
                new Person { EmailAddress = "D" },
                new Person { EmailAddress = "B" },
                new Person { EmailAddress = "Z" },
                new Person { EmailAddress = "X" },
                new Person { EmailAddress = "E" }
            }.AsQueryable();

            //     var _A = new QueryableUnion<Person>(_PersonQuery1, _PersonQuery2);

            //     var _X = _A.ProjectTo<CustomerDto>(new AutoMapper.MapperConfiguration(opts => opts.CreateMap<Person, CustomerDto>().ForMember(dest => dest.EmailAddress, opts => opts.MapFrom(src => src.EmailAddress)).ForAllOtherMembers(opts => opts.Ignore())));
            //     var y = _X.ToList();

            //var _PersonQuery1 = new[]
            //{
            //    new Person { EmailAddress = "J" },
            //    new Person { EmailAddress = "K" },
            //    new Person { EmailAddress = "L" },
            //    new Person { EmailAddress = "M" },
            //    new Person { EmailAddress = "N" }
            //}.AsQueryable();

            //var _PersonQuery2 = new Person[] { }.AsQueryable();
            //var _PersonQuery3 = new Person[] { }.AsQueryable();
            //var _PersonQuery4 = new Person[] { }.AsQueryable();

            //var _PersonQuery5 = new[]
            //{
            //    new Person { EmailAddress = "A", FirstName = "B" },
            //    new Person { EmailAddress = "A", FirstName = "A" },
            //    new Person { EmailAddress = "C" },
            //    new Person { EmailAddress = "E" }
            //}.AsQueryable();

            //var _PersonQuery6 = new[]
            //{
            //    new Person { EmailAddress = "B" },
            //    new Person { EmailAddress = "D" }
            //}.AsQueryable();

            //var _PersonQuery7 = new[]
            //{
            //    new Person { EmailAddress = "3" },
            //    new Person { EmailAddress = "2" },
            //    new Person { EmailAddress = "1" }
            //}.AsQueryable();

            //var _QueryableUnion = new QueryableUnion<Person>(_PersonQuery1, _PersonQuery2, _PersonQuery3, _PersonQuery4, _PersonQuery5, _PersonQuery6, _PersonQuery7);
            ////var _X = _QueryableUnion.Any();

            ////var _X = _QueryableUnion.OrderBy(p => p.EmailAddress).ThenBy(p => p.FirstName).Take(1).Select(p => new { p.EmailAddress }).ToList();
            ////var _X = _QueryableUnion.OrderBy(p => p.EmailAddress).Select(p => p.EmailAddress).OrderByDescending(x => x).ToList();
            ////var _A = _QueryableUnion.OrderBy(p => new X(p)/*.EmailAddress, "A")*/).ToList();
            //var _A = _QueryableUnion.OrderBy(p => p.EmailAddress);
            //var _B = _A.Select(p => new { p.EmailAddress, p.FirstName, p.LastName });
            ////var _C = _A.Where(x => x.EmailAddress == "A");

            //xxx //This is currently working. It might make sense to change the internal implementation to be all expression based, instead of wrapping queryables.
            //    // Before any refactoring, clean up the code and commit it. It's a slight improvement. maybe make it not re-apply the expression tree if a projection occurs (check to see if the types are assignable)
            //    // All expression based means that instead of storing queryables, the queryables can be encapsulated into an expression tree.
            //    // This would require a "public" expression, and a "private" expression. Actually, maybe the private expression isn't required.
            //    // The expression tree is what's compiled and invoked. What that expression actually *means* is totally irrelevant for the consumer.

            //var _Z = _A.ToList();
            //var aa = 0;

            //var _A = new XXX();
            //var _B = new XYZ();
            //var _C = _A.Concat(_B);

            //var _A = _PersonQuery1.Concat(_PersonQuery2);

            var x = new ConcatenatedQueryable<Person>(_PersonQuery1, _PersonQuery2);
            var y = x.Select(p => p.EmailAddress);
            var z = y.OrderBy(e => e);

            var a = new ConcatenatedQueryable<string>(z, new[] { "T", "S", "R" }.AsQueryable());
            var b = a.OrderBy(s => s);

            //var xx = new ConcatenatedQueryableExpressionVisitor();
            //var yy = xx.Visit(b.Expression);

            //var xxx = b.GetEnumerator();


            //xxx // This looks like it's working, but it's probably not. The reason is the first concatenation is being sorted together (by the orderby s => s).
            //    // This means that the individual queries are being executed, then the second sort is being applied. If those were hits to the DB (and projections),
            //    // the DB would be loading more than necessary. This needs to be investigated - the current approach is likely wrong.
            //    // When a DuplicateTree encounters a DuplicateTree internally, it needs to apply it's expression to that DuplicateTree.

            //var xyz = 0;

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


    //public class XXX : IQueryable<string>
    //{
    //    public Type ElementType => throw new NotImplementedException();

    //    public System.Linq.Expressions.Expression Expression => throw new NotImplementedException();

    //    public IQueryProvider Provider => throw new NotImplementedException();

    //    public IEnumerator<string> GetEnumerator() => throw new NotImplementedException();
    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => throw new NotImplementedException();
    //}

    //public class XYZ : IQueryable<string>
    //{
    //    public Type ElementType => throw new NotImplementedException();

    //    public System.Linq.Expressions.Expression Expression => throw new NotImplementedException();

    //    public IQueryProvider Provider => throw new NotImplementedException();

    //    public IEnumerator<string> GetEnumerator() => throw new NotImplementedException();
    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => throw new NotImplementedException();
    //}


    public class X : IComparable
    {
        private readonly string m_A;
        private readonly string m_B;

        public X(Person p)
        {
            //this.m_A = a;
            //this.m_B = b;
        }

        public int CompareTo(object obj)
        {
            var _X = this.m_A.CompareTo(((X)obj).m_A);
            return _X != 0 ? _X : this.m_B.CompareTo(((X)obj).m_B);
        }

    }

    public class TempPersistenceContext : IPersistenceContext
    {

        #region - - - - - - IPersistenceContext Implementation - - - - - -

        public Task<EntityID> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity
            => throw new NotImplementedException();

        public Task<TEntity> FindAsync<TEntity>(EntityID entityID, CancellationToken cancellationToken) where TEntity : class, IEntity
            => throw new NotImplementedException();

        public Task<IQueryable<TEntity>> GetEntitiesAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class, IEntity
            => throw new NotImplementedException();

        public Task RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity
            => throw new NotImplementedException();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => throw new NotImplementedException();

        #endregion IPersistenceContext Implementation

    }

}
