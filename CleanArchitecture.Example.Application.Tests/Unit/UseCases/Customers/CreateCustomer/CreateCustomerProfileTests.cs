using AutoMapper;
using CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerProfileTests
    {

        #region - - - - - - Profile Configuration Tests - - - - - -

        [Fact]
        public void CreateCustomerProfile_ConfigurationValidation_Successful()
            => new MapperConfiguration(cfg => cfg.AddProfile<CreateCustomerProfile>()).AssertConfigurationIsValid();

        #endregion Profile Configuration Tests

    }

}
