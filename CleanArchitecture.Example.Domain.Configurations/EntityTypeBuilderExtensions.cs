using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Example.Domain.Configurations
{

    public static class EntityTypeBuilderExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static void HasEntityID<TEntity, TID>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity
            => _ = builder
                    .Property(e => e.ID)
                    .HasConversion(
                        entityID => EntityIDProvider.GetValueFromEntityID<TID>(entityID),
                        dbVal => EntityIDProvider.GetEntityIDWithValue(dbVal));

        #endregion Methods

    }

}
