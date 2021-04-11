using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Application.UseCases.Employees.GetEmployeeRoles
{

    public class EmployeeRoleDto
    {

        #region - - - - - - Properties - - - - - -

        public EntityID EmployeeRoleID { get; set; }

        public string Name { get; set; }

        #endregion Properties

    }

}
