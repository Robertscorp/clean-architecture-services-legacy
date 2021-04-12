using AutoMapper;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Application.UseCases.Users.CreateUser
{

    public class CreateUserInteractor : IUseCaseInteractor<CreateUserRequest, UserDto>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateUserInteractor(IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public async Task HandleAsync(CreateUserRequest request, IPresenter<UserDto> presenter, CancellationToken cancellationToken)
        {
            var _Employee = this.m_Mapper.Map<Employee>(request);

            _ = await this.m_PersistenceContext.AddAsync(_Employee, cancellationToken);
            await presenter.PresentAsync(this.m_Mapper.Map<UserDto>(_Employee), cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
