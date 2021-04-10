using AutoMapper;
using CleanArchitecture.Example.Application.UseCases.Person.GetGenders;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Person.GetGenders
{

    public class GetGendersProfileTests
    {

        #region - - - - - - Profile Configuration Tests - - - - - -

        [Fact]
        public void GetGenderProfile_ConfigurationValidation_Successful()
            => new MapperConfiguration(cfg => cfg.AddProfile<GetGendersProfile>()).AssertConfigurationIsValid();

        #endregion Profile Configuration Tests

    }

}
