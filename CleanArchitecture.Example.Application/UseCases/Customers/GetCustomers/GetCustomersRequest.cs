using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Services.Pipeline;
using System.Linq;

namespace CleanArchitecture.Example.Application.UseCases.Customers.GetCustomers
{

    public class GetCustomersRequest : IUseCaseRequest<IQueryable<CustomerDto>>
    {
    }

}
