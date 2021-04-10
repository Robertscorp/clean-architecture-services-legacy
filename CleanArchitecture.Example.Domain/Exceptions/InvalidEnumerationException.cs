using System;

namespace CleanArchitecture.Example.Domain.Exceptions
{

    public class InvalidEnumerationException : Exception
    {

        #region - - - - - - Constructors - - - - - -

        public InvalidEnumerationException(int value, Type enumerationType) : base($"{value} is not valid for {enumerationType.Name}.") { }

        #endregion Constructors

    }

}
