using AutoMapper;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Domain.Enumerations;
using CleanArchitecture.Services.Enumerations;
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

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GetEmployeeRolesInteractor(IMapper mapper)
            => this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public Task HandleAsync(GetEmployeeRolesRequest request, IPresenter<IQueryable<EmployeeRoleDto>> presenter, CancellationToken cancellationToken)
            => presenter.PresentAsync(this.m_Mapper.Map<List<EmployeeRoleDto>>(Enumeration.GetAll<EmployeeRoleEnumeration>()).AsQueryable(), cancellationToken);

        #endregion IUseCaseInteractor Implementation

    }

}
