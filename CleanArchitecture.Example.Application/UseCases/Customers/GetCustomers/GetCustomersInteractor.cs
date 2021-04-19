using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Application.UseCases.Customers.GetCustomers
{

    public class GetCustomersInteractor : IUseCaseInteractor<GetCustomersRequest, IQueryable<CustomerDto>>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GetCustomersInteractor(IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public async Task HandleAsync(GetCustomersRequest request, IPresenter<IQueryable<CustomerDto>> presenter, CancellationToken cancellationToken)
        {
            var _Customers = await this.m_PersistenceContext.GetEntitiesAsync<Customer>(cancellationToken);

            await presenter.PresentAsync(_Customers.ProjectTo<CustomerDto>(this.m_Mapper.ConfigurationProvider), cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
