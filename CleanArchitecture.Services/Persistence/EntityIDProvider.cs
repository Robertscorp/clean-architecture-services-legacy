using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Internal;

namespace CleanArchitecture.Services.Persistence
{

    public static class EntityIDProvider
    {

        #region - - - - - - Methods - - - - - -

        //public static bool TryUpdateEntityIDForEntity<TEntity>(TEntity entity, out EntityID entityID) where TEntity : IEntity
        //{
        //    var _

        //    if (entity.ID == null)
        //}
        //=> new InternalEntityID { Data = new InternalUnpersistedEntityEntityIDData<TEntity>(entity) };

        public static EntityID GetEntityID()
            => new InternalEntityID();

        public static EntityID GetEntityIDWithValue<TValue>(TValue value)
            => new InternalEntityID { Data = new InternalDeserialisedEntityIDData<TValue>(value) };

        public static TValue GetValueFromEntityID<TValue>(EntityID entityID)
            => entityID is InternalEntityID _InternalEntityID
                ? _InternalEntityID.Data is InternalEntityIDData<TValue> _InternalEntityIDData ? _InternalEntityIDData.Value : default
                : default;

        #endregion Methods

    }

}
