﻿using CleanArchitecture.Services.Internal;
using System;

namespace CleanArchitecture.Services.Entities
{

    public abstract class StaticEntity : IComparable
    {

        #region - - - - - - Constructors - - - - - -

        protected StaticEntity(string name, long value)
        {
            this.ID = new InternalEntityID { Data = (InternalEntityIDData)Activator.CreateInstance(typeof(InternalEntityIDData<,>).MakeGenericType(this.GetType(), typeof(long)), value) };
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

        private long GetEntityIDValue()
            => ((InternalEntityIDData<long>)((InternalEntityID)this.ID).Data).Value;

        public override int GetHashCode()
            => this.ID.GetHashCode();

        /// <summary>
        /// Determines if this instance will be returned from the Static Entity Context.
        /// </summary>
        protected internal virtual bool IsContextEntity()
            => true;

        public override string ToString()
            => this.Name;

        #endregion Methods

    }

}
