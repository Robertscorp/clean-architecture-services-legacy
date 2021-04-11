using CleanArchitecture.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Services.Enumerations
{

    public abstract class Enumeration : IComparable
    {

        #region - - - - - - Fields - - - - - -

        private readonly string m_Name;
        private readonly int m_Value;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        protected Enumeration(string name, int value)
        {
            this.m_Name = name ?? throw new ArgumentNullException(nameof(name));
            this.m_Value = value;
        }

        #endregion Constructors

        #region - - - - - - IComparable Implementation - - - - - -

        public int CompareTo(object obj)
            => obj is Enumeration _Enumeration ? this.m_Value.CompareTo(_Enumeration.m_Value) : -1;

        #endregion IComparable Implementation

        #region - - - - - - Methods - - - - - -

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration _Enumeration))
                return false;

            if (this.GetType() != _Enumeration.GetType())
                return false;

            return this.m_Value == _Enumeration.m_Value;
        }

        public static TEnumeration FromEntityID<TEnumeration>(EntityID entityID) where TEnumeration : Enumeration
            => entityID is EnumerationEntityID _EnumerationEntityID
                ? (Enumeration)_EnumerationEntityID as TEnumeration
                    ?? throw new InvalidEnumerationException(entityID, typeof(TEnumeration))
                : throw new InvalidEnumerationException(entityID, typeof(TEnumeration));

        public static TEnumeration Get<TEnumeration>(int value) where TEnumeration : Enumeration
            => GetAll<TEnumeration>()
                .SingleOrDefault(e => e.m_Value == value)
                    ?? throw new InvalidEnumerationException(value, typeof(TEnumeration));

        public static IEnumerable<TEnumeration> GetAll<TEnumeration>() where TEnumeration : Enumeration
            => typeof(TEnumeration)
                .GetFields()
                .Where(f => typeof(Enumeration).IsAssignableFrom(f.FieldType))
                .Select(f => f.GetValue(null))
                .Cast<TEnumeration>();

        public override int GetHashCode()
            => this.m_Value.GetHashCode();

        public EntityID ToEntityID()
            => (EnumerationEntityID)this;

        public override string ToString()
            => this.m_Name;

        #endregion Methods

    }

}
