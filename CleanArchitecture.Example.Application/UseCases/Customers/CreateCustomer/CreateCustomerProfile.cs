using AutoMapper;
using CleanArchitecture.Example.Application.Extensions;
using CleanArchitecture.Example.Domain.Entities;

namespace CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public CreateCustomerProfile()
        {
            _ = this.CreateMap<CreateCustomerRequest, Customer>()
                    .ForMember(dest => dest.CustomerDetails, opts => opts.MapFrom(src => src))
                    .ForMember(dest => dest.Vehicles, opts => opts.Ignore());

            _ = this.CreateMap<CreateCustomerRequest, Person>()
                    .ForMember(dest => dest.Gender, opts => opts.MapFromEntityID(src => src.GenderID));
        }

        #endregion Constructors

    }

}
