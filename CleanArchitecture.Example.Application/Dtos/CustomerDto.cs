using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Application.Dtos
{

    public class CustomerDto
    {

        #region - - - - - - Properties - - - - - -

        public EntityID CustomerID { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public EntityID GenderID { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        #endregion Properties

    }

}
