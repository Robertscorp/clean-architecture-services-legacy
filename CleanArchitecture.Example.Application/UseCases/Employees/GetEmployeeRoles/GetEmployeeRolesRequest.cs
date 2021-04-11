using CleanArchitecture.Services.Pipeline;
using System.Linq;

namespace CleanArchitecture.Example.Application.UseCases.Employees.GetEmployeeRoles
{

    public class GetEmployeeRolesRequest : IUseCaseRequest<IQueryable<EmployeeRoleDto>>
    {
    }

}
