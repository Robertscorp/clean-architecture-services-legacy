using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Application.UseCases.Users.GetUsers
{

    public class GetUsersInteractor : IUseCaseInteractor<GetUsersRequest, IQueryable<UserDto>>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GetUsersInteractor(IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public async Task HandleAsync(GetUsersRequest request, IPresenter<IQueryable<UserDto>> presenter, CancellationToken cancellationToken)
        {
            var _Employees = await this.m_PersistenceContext.GetEntitiesAsync<Employee>(cancellationToken);

            await presenter.PresentAsync(_Employees.ProjectTo<UserDto>(this.m_Mapper.ConfigurationProvider), cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
