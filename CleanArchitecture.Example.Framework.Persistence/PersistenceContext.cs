using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extensions;
using CleanArchitecture.Services.Infrastructure;
using CleanArchitecture.Services.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Framework.Persistence
{

    public class PersistenceContext : IPersistenceContext
    {

        #region - - - - - - Fields - - - - - -

        private readonly List<object> m_AllEntities = new List<object>();

        private int m_IDSeed = 999;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public PersistenceContext()
        {
            var _Customer1 = new Customer
            {
                ID = EntityIDProvider.GetEntityIDWithValue(1L),
                CustomerDetails = new Person
                {
                    EmailAddress = "Amanda.Hugnkiss@maininator.com",
                    FirstName = "Amanda",
                    Gender = Gender.Female,
                    ID = EntityIDProvider.GetEntityIDWithValue(1L),
                    LastName = "Hugnkiss",
                    MobileNumber = "0400000000"
                }
            };
            var _Customer2 = new Customer
            {
                ID = EntityIDProvider.GetEntityIDWithValue(2L),
                CustomerDetails = new Person
                {
                    EmailAddress = "Ivana.Tinkle@maininator.com",
                    FirstName = "Ivana",
                    Gender = Gender.Female,
                    ID = EntityIDProvider.GetEntityIDWithValue(2L),
                    LastName = "Tinkle",
                    MobileNumber = "0400000001"
                }
            };
            var _Customer3 = new Customer
            {
                ID = EntityIDProvider.GetEntityIDWithValue(3L),
                CustomerDetails = new Person
                {
                    EmailAddress = "Mahatma.Cote@maininator.com",
                    FirstName = "Mahatma",
                    Gender = Gender.Male,
                    ID = EntityIDProvider.GetEntityIDWithValue(3L),
                    LastName = "Cote",
                    MobileNumber = "0400000002"
                }
            };

            this.m_AllEntities.Add(_Customer1);
            this.m_AllEntities.Add(_Customer2);
            this.m_AllEntities.Add(_Customer3);

            this.m_AllEntities.Add(_Customer1.CustomerDetails);
            this.m_AllEntities.Add(_Customer2.CustomerDetails);
            this.m_AllEntities.Add(_Customer3.CustomerDetails);
        }

        #endregion Constructors

        #region - - - - - - IPersistenceContext Implementation - - - - - -

        public Task<EntityID> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            this.m_AllEntities.Add(entity);

            return Task.FromResult(this.TrackEntity(entity));
        }

        public Task<bool> ExistsAsync<TEntity>(EntityID entityID, CancellationToken cancellationToken) where TEntity : class
            => Task.FromResult((StaticEntityContext.Find<TEntity>(entityID) ?? this.m_AllEntities.OfType<TEntity>().SingleOrDefault(e => Equals(e.GetType().GetProperty("ID").GetValue(e), entityID))) != null);

        public Task<TEntity> FindAsync<TEntity>(EntityID entityID, CancellationToken cancellationToken) where TEntity : class
            => Task.FromResult(StaticEntityContext.Find<TEntity>(entityID) ?? this.m_AllEntities.OfType<TEntity>().SingleOrDefault(e => Equals(e.GetType().GetProperty("ID").GetValue(e), entityID)));

        public Task<IQueryable<TEntity>> GetEntitiesAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class
            => Task.FromResult((StaticEntityContext.GetEntities<TEntity>() ?? this.m_AllEntities.OfType<TEntity>()).AsQueryable());

        public Task RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            _ = this.m_AllEntities.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => Task.FromResult(0);

        #endregion IPersistenceContext Implementation

        #region - - - - - - Methods - - - - - -

        private static IEntity[] GetNestedEntities<TEntity>(TEntity entity)
        {
            if (!s_GetNestedEntitiesByType.TryGetValue(entity.GetType(), out var _GetEntitiesFunc))
            {
                var _NestedEntityFunctions = entity
                                                .GetType()
                                                .GetProperties()
                                                .Where(p => typeof(IEntity).IsAssignableFrom(p.PropertyType))
                                                .Select(p => p.AsFunction<TEntity, IEntity>())
                                                .ToList();

                _GetEntitiesFunc = entity => _NestedEntityFunctions.Select(f => f.Invoke((TEntity)entity)).ToArray();
                s_GetNestedEntitiesByType.Add(entity.GetType(), _GetEntitiesFunc);
            }

            return _GetEntitiesFunc.Invoke(entity);
        }
        private static Dictionary<Type, Func<object, IEntity[]>> s_GetNestedEntitiesByType = new Dictionary<Type, Func<object, IEntity[]>>();

        private EntityID TrackEntity<TEntity>(TEntity entity)
        {
            if (!(entity is IEntity _Entity))
                return null;

            _Entity.ID = EntityIDProvider.GetEntityIDWithValue(Interlocked.Increment(ref this.m_IDSeed));

            foreach (var _NestedEntity in GetNestedEntities(entity))
                _ = this.TrackEntity(_NestedEntity);

            return _Entity.ID;
        }

        #endregion Methods

    }

}
