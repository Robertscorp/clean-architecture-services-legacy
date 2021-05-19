using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer;
using CleanArchitecture.Example.Application.UseCases.Customers.DeleteCustomer;
using CleanArchitecture.Example.Application.UseCases.Customers.GetCustomers;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Extended.Presenters;
using CleanArchitecture.Services.Pipeline;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.InterfaceAdapters.Controllers
{

    public class CustomerController
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInvoker m_UseCaseInvoker;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CustomerController(IUseCaseInvoker useCaseInvoker)
            => this.m_UseCaseInvoker = useCaseInvoker ?? throw new ArgumentNullException(nameof(useCaseInvoker));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task CreateCustomerAsync(CreateCustomerRequest request, IPresenter<CustomerDto> presenter, CancellationToken cancellationToken)
            => this.m_UseCaseInvoker.InvokeUseCaseAsync(request, presenter, cancellationToken);

        public Task DeleteCustomerAsync(DeleteCustomerRequest request, IPresenter<CustomerDto> presenter, CancellationToken cancellationToken)
            => this.m_UseCaseInvoker.InvokeUseCaseAsync(request, presenter, cancellationToken);

        public Task GetCustomerAsync(EntityID customerID, IPresenter<CustomerDto> presenter, CancellationToken cancellationToken)
            => this.m_UseCaseInvoker.InvokeUseCaseAsync(new GetCustomersRequest(), new SingleEntityPresenter<CustomerDto>(customerID, dto => dto.CustomerID, presenter), cancellationToken);

        public Task GetCustomersAsync(IPresenter<IQueryable<CustomerDto>> presenter, CancellationToken cancellationToken)
            => this.m_UseCaseInvoker.InvokeUseCaseAsync(new GetCustomersRequest(), presenter, cancellationToken);

        #endregion Methods

    }

}
