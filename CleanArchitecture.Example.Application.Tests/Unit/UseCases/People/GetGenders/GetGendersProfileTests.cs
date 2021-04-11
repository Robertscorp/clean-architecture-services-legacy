using AutoMapper;
using CleanArchitecture.Example.Application.UseCases.People.GetGenders;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.People.GetGenders
{

    public class GetGendersProfileTests
    {

        #region - - - - - - Profile Configuration Tests - - - - - -

        [Fact]
        public void GetGendersProfile_ConfigurationValidation_Successful()
            => new MapperConfiguration(cfg => cfg.AddProfile<GetGendersProfile>()).AssertConfigurationIsValid();

        #endregion Profile Configuration Tests

    }

}
