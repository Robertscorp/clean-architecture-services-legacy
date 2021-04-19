using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerInteractor : IUseCaseInteractor<CreateCustomerRequest, CustomerDto>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateCustomerInteractor(IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public async Task HandleAsync(CreateCustomerRequest request, IPresenter<CustomerDto> presenter, CancellationToken cancellationToken)
        {
            var _Customer = this.m_Mapper.Map<Customer>(request);

            _ = await this.m_PersistenceContext.AddAsync(_Customer, cancellationToken);
            await presenter.PresentAsync(this.m_Mapper.Map<CustomerDto>(_Customer), cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
