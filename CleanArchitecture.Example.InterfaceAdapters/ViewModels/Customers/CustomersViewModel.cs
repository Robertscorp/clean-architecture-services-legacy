using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.Customers.GetCustomers;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.InterfaceAdapters.ViewModels.Customers
{

    public class CustomersViewModel : IPresenter<IQueryable<CustomerDto>>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IUseCaseInvoker m_UseCaseInvoker;

        private readonly List<ExistingCustomerViewModel> m_Customers = new List<ExistingCustomerViewModel>();

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CustomersViewModel(IMapper mapper, IUseCaseInvoker useCaseInvoker)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_UseCaseInvoker = useCaseInvoker ?? throw new ArgumentNullException(nameof(useCaseInvoker));
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        // Callbacks

        public Action<ExistingCustomerViewModel> OnCustomerAdded { get; set; }


        // ViewModels

        public ReadOnlyCollection<ExistingCustomerViewModel> Customers => new ReadOnlyCollection<ExistingCustomerViewModel>(this.m_Customers);

        #endregion Properties

        #region - - - - - - IPresenter Implementation - - - - - -

        public Task PresentAsync(IQueryable<CustomerDto> response, CancellationToken cancellationToken)
        {
            this.m_Customers.AddRange(response.ProjectTo<ExistingCustomerViewModel>(this.m_Mapper.ConfigurationProvider));
            return Task.CompletedTask;
        }

        public Task PresentEntityNotFoundAsync(EntityID entityID, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        public Task PresentValidationFailureAsync(ValidationResult validationResult, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        #endregion IPresenter Implementation

        #region - - - - - - Methods - - - - - -

        public void AddExistingCustomer(ExistingCustomerViewModel customer)
        {
            this.m_Customers.Add(customer);
            this.OnCustomerAdded?.Invoke(customer);
        }

        public NewCustomerViewModel CreateNewCustomer()
            => new NewCustomerViewModel(this.m_Mapper, this.m_UseCaseInvoker)
            {
                //OnCustomerCreated = customer => this.AddExistingCustomer(customer)
            };

        public async Task InitialiseAsync(CancellationToken cancellationToken)
            => await this.m_UseCaseInvoker.InvokeUseCaseAsync(new GetCustomersRequest(), this, cancellationToken);

        #endregion Methods

    }
}
