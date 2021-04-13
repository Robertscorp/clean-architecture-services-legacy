using CleanArchitecture.Services.Entities;
using System;

namespace CleanArchitecture.Services.Enumerations
{

    public class InvalidEnumerationException : Exception
    {

        #region - - - - - - Constructors - - - - - -

        public InvalidEnumerationException(EntityID entityID, Type enumerationType) : base($"{nameof(entityID)} is not for {enumerationType.Name}.") { }

        public InvalidEnumerationException(int value, Type enumerationType) : base($"{value} is not valid for {enumerationType.Name}.") { }

        #endregion Constructors

    }

}
