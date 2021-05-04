using AutoMapper;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Application.UseCases.Employees.GetEmployeeRoles
{

    public class GetEmployeeRolesInteractor : IUseCaseInteractor<GetEmployeeRolesRequest, IQueryable<EmployeeRoleDto>>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GetEmployeeRolesInteractor(IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public async Task HandleAsync(GetEmployeeRolesRequest request, IPresenter<IQueryable<EmployeeRoleDto>> presenter, CancellationToken cancellationToken)
            => await presenter.PresentAsync(this.m_Mapper.Map<List<EmployeeRoleDto>>(await this.m_PersistenceContext.GetEntitiesAsync<EmployeeRole>(cancellationToken)).AsQueryable(), cancellationToken);

        #endregion IUseCaseInteractor Implementation

    }

}
