using AutoMapper;
using CleanArchitecture.Example.Application.Infrastructure.Mapping;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.Infrastructure.Mapping
{

    public class DtoMappingProfileTests
    {

        #region - - - - - - Profile Configuration Tests - - - - - -

        [Fact]
        public void DtoMappingProfile_ConfigurationValidation_Successful()
            => new MapperConfiguration(cfg => cfg.AddProfile<DtoMappingProfile>()).AssertConfigurationIsValid();

        #endregion Profile Configuration Tests

    }

}
