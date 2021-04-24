using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;

namespace CleanArchitecture.Example.Application.UseCases.Customers.DeleteCustomer
{

    public class DeleteCustomerRequest : IUseCaseRequest<CustomerDto>
    {

        #region - - - - - - Properties - - - - - -

        public EntityID CustomerID { get; set; }

        #endregion Properties

    }

}
