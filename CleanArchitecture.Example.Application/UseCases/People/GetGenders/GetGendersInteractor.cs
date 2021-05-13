using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Application.UseCases.People.GetGenders
{

    public class GetGendersInteractor : IUseCaseInteractor<GetGendersRequest, IQueryable<GenderDto>>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GetGendersInteractor(IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public async Task HandleAsync(GetGendersRequest request, IPresenter<IQueryable<GenderDto>> presenter, CancellationToken cancellationToken)
            => await presenter.PresentAsync((await this.m_PersistenceContext.GetEntitiesAsync<Gender>(cancellationToken)).ProjectTo<GenderDto>(this.m_Mapper.ConfigurationProvider), cancellationToken);

        #endregion IUseCaseInteractor Implementation

    }

}
