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

        private static ConcurrentDictionary<Type, object[]> s_EntitiesByType;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public static TEntity Find<TEntity>(EntityID entityID) where TEntity : class
            => (TEntity)GetEntitiesInternal<TEntity>().SingleOrDefault(e => Equals(((StaticEntity)e).ID, entityID));

        public static TEntity[] GetEntities<TEntity>() where TEntity : class
            => (TEntity[])GetEntitiesInternal<TEntity>();

        private static object[] GetEntitiesInternal<TEntity>() where TEntity : class
        {
            if (s_EntitiesByType == null)
                _ = Interlocked.CompareExchange(ref s_EntitiesByType, new ConcurrentDictionary<Type, object[]>(), null);

            if (!s_EntitiesByType.TryGetValue(typeof(TEntity), out var _StaticEntities))
                _StaticEntities = s_EntitiesByType.GetOrAdd(
                                    typeof(TEntity),
                                    typeof(TEntity)
                                        .GetFields()
                                        .Where(f => typeof(TEntity).IsAssignableFrom(f.FieldType))
                                        .Select(f => f.GetValue(null))
                                        .OfType<StaticEntity>()
                                        .OfType<TEntity>()
                                        .ToArray());

            return _StaticEntities;
        }

        #endregion Methods

    }

}
