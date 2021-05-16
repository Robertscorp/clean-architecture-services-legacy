using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Application.UseCases.Customers.DeleteCustomer
{

    public class DeleteCustomerInteractor : IUseCaseInteractor<DeleteCustomerRequest, CustomerDto>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public DeleteCustomerInteractor(IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public async Task HandleAsync(DeleteCustomerRequest request, IPresenter<CustomerDto> presenter, CancellationToken cancellationToken)
        {
            var _Customer = await this.m_PersistenceContext.FindAsync<Customer>(request.CustomerID, cancellationToken);
            if (_Customer == null)
            {
                await presenter.PresentEntityNotFoundAsync(request.CustomerID, cancellationToken);
                return;
            }

            await this.m_PersistenceContext.RemoveAsync(_Customer, cancellationToken);
            await presenter.PresentAsync(this.m_Mapper.Map<CustomerDto>(_Customer), cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
