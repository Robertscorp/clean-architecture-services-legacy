using AutoMapper;
using CleanArchitecture.Example.Application.Infrastructure.Mapping;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public CreateCustomerProfile()
        {
            _ = this.CreateMap<CreateCustomerRequest, Customer>()
                    .ForMember(dest => dest.CustomerDetails, opts => opts.MapFrom(src => src))
                    .ForMember(dest => dest.ID, opts => opts.Ignore())
                    .ForMember(dest => dest.Vehicles, opts => opts.Ignore());

            _ = this.CreateMap<CreateCustomerRequest, Person>()
                    .ForMember(dest => dest.Gender, opts => opts.ConvertUsing<EntityIDConverter<Gender>, EntityID>(src => src.GenderID))
                    .ForMember(dest => dest.ID, opts => opts.Ignore());
        }

        #endregion Constructors

    }

}
