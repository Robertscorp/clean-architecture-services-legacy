using CleanArchitecture.Services.Entities;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace CleanArchitecture.Services.Infrastructure
{

    public static class StaticEntityContext
    {

        #region - - - - - - Fields - - - - - -

        private static ConcurrentDictionary<Type, IEntity[]> s_EntitiesByType;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public static TEntity Find<TEntity>(EntityID entityID) where TEntity : class, IEntity
            => (TEntity)GetEntitiesInternal<TEntity>()?.SingleOrDefault(e => Equals(e.ID, entityID));

        public static TEntity[] GetEntities<TEntity>() where TEntity : class, IEntity
            => (TEntity[])GetEntitiesInternal<TEntity>();

        private static IEntity[] GetEntitiesInternal<TEntity>() where TEntity : class, IEntity
        {
            if (s_EntitiesByType == null)
                _ = Interlocked.CompareExchange(ref s_EntitiesByType, new ConcurrentDictionary<Type, IEntity[]>(), null);

            if (!s_EntitiesByType.TryGetValue(typeof(TEntity), out var _StaticEntities))
                _StaticEntities = s_EntitiesByType.GetOrAdd(
                                    typeof(TEntity),
                                    typeof(StaticEntity).IsAssignableFrom(typeof(TEntity))
                                        ? typeof(TEntity)
                                            .GetFields()
                                            .Where(f => typeof(TEntity).IsAssignableFrom(f.FieldType))
                                            .Select(f => f.GetValue(null))
                                            .OfType<StaticEntity>()
                                            .Where(e => e.IsContextEntity())
                                            .OfType<TEntity>()
                                            .ToArray()
                                        : null
                                    );

            return _StaticEntities;
        }

        #endregion Methods

    }

}
