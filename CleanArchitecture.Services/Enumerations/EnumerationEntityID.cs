using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Services.Enumerations
{

    internal class EnumerationEntityID : EntityID
    {

        #region - - - - - - Fields - - - - - -

        private Enumeration m_Enumeration;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override bool Equals(object obj)
            => obj is EnumerationEntityID _EntityID && this.m_Enumeration.Equals(_EntityID.m_Enumeration);

        public override int GetHashCode()
            => this.m_Enumeration.GetHashCode();

        public static implicit operator EnumerationEntityID(Enumeration enumeration)
            => new EnumerationEntityID { m_Enumeration = enumeration };

        public static implicit operator Enumeration(EnumerationEntityID entityID)
            => entityID?.m_Enumeration;

        #endregion Methods

    }

}
