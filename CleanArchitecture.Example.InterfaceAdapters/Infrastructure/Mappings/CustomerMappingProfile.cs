using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.InterfaceAdapters.ViewModels.Customers;

namespace CleanArchitecture.Example.InterfaceAdapters.Infrastructure.Mappings
{

    public class CustomerMappingProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public CustomerMappingProfile()
        {
            _ = this.CreateMap<CustomerDto, ExistingCustomerViewModel>();
        }

        #endregion Constructors

    }

}
