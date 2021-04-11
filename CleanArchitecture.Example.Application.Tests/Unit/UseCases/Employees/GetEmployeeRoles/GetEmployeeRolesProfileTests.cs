using AutoMapper;
using CleanArchitecture.Example.Application.UseCases.Employees.GetEmployeeRoles;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Employees.GetEmployeeRoles
{

    public class GetEmployeeRolesProfileTests
    {

        #region - - - - - - Profile Configuration Tests - - - - - -

        [Fact]
        public void GetEmployeeRolesProfile_ConfigurationValidation_Successful()
            => new MapperConfiguration(cfg => cfg.AddProfile<GetEmployeeRolesProfile>()).AssertConfigurationIsValid();

        #endregion Profile Configuration Tests

    }

}
