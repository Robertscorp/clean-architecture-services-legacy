using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Services.Entities
{

    public abstract class StaticEntity : IComparable
    {

        #region - - - - - - Constructors - - - - - -

        protected StaticEntity(string name, long value)
        {
            this.ID = new StaticEntityID(value);
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public EntityID ID { get; }

        public string Name { get; }

        #endregion Properties

        #region - - - - - - IComparable Implementation - - - - - -

        public int CompareTo(object obj)
            => obj is StaticEntity _Enumeration ? this.GetEntityIDValue().CompareTo(_Enumeration.GetEntityIDValue()) : -1;

        #endregion IComparable Implementation

        #region - - - - - - Methods - - - - - -

        public override bool Equals(object obj)
            => obj is StaticEntity _Enumeration && Equals(this.ID, _Enumeration.ID);

        public static TEntity Get<TEntity>(EntityID entityID) where TEntity : StaticEntity
            => GetAll<TEntity>().SingleOrDefault(e => Equals(e.ID, entityID));

        public static IEnumerable<TEntity> GetAll<TEntity>() where TEntity : StaticEntity
            => typeof(TEntity)
                .GetFields()
                .Where(f => typeof(StaticEntity).IsAssignableFrom(f.FieldType))
                .Select(f => f.GetValue(null))
                .Cast<TEntity>();

        private long GetEntityIDValue()
            => ((StaticEntityID)this.ID).Value;

        public override int GetHashCode()
            => this.ID.GetHashCode();

        public override string ToString()
            => this.Name;

        #endregion Methods

    }

}
