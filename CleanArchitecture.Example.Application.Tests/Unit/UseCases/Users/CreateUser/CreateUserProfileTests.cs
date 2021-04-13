using AutoMapper;
using CleanArchitecture.Example.Application.UseCases.Users.CreateUser;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Users.CreateUser
{

    public class CreateUserProfileTests
    {

        #region - - - - - - Profile Configuration Tests - - - - - -

        [Fact]
        public void CreateUserProfile_ConfigurationValidation_Successful()
            => new MapperConfiguration(cfg => cfg.AddProfile<CreateUserProfile>()).AssertConfigurationIsValid();

        #endregion Profile Configuration Tests

    }

}
