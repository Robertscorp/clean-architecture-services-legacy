using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;

namespace CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerRequest : IUseCaseRequest<CustomerDto>
    {

        #region - - - - - - Properties - - - - - -

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public EntityID GenderID { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        #endregion Properties

    }

}
