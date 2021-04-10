using CleanArchitecture.Example.Domain.Enumerations;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Person
    {

        #region - - - - - - Properties - - - - - -

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public GenderEnumeration Gender { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        #endregion Properties

    }

}
