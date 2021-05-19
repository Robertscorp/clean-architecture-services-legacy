using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Domain.Entities;

namespace CleanArchitecture.Example.Application.Infrastructure.Mapping
{

    public class DtoMappingProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public DtoMappingProfile()
        {
            _ = this.CreateMap<Customer, CustomerDto>()
                    .ForMember(dest => dest.CustomerID, opts => opts.MapFrom(src => src.ID))
                    .ForMember(dest => dest.EmailAddress, opts => opts.MapFrom(src => src.CustomerDetails.EmailAddress))
                    .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.CustomerDetails.FirstName))
                    .ForMember(dest => dest.GenderID, opts => opts.MapFrom(src => src.CustomerDetails.Gender.ID))
                    .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.CustomerDetails.LastName))
                    .ForMember(dest => dest.MobileNumber, opts => opts.MapFrom(src => src.CustomerDetails.MobileNumber));

            _ = this.CreateMap<Employee, UserDto>()
                    .ForMember(dest => dest.EmployeeID, opts => opts.MapFrom(src => src.ID))
                    .ForMember(dest => dest.EmployeeRoleID, opts => opts.MapFrom(src => src.Role.ID))
                    .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.EmployeeDetails.FirstName))
                    .ForMember(dest => dest.GenderID, opts => opts.MapFrom(src => src.EmployeeDetails.Gender.ID))
                    .ForMember(dest => dest.HashedPassword, opts => opts.MapFrom(src => src.User.HashedPassword))
                    .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.EmployeeDetails.LastName))
                    .ForMember(dest => dest.PersonID, opts => opts.MapFrom(src => src.EmployeeDetails.ID))
                    .ForMember(dest => dest.UserID, opts => opts.MapFrom(src => src.User.ID))
                    .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.UserName));
        }

        #endregion Constructors

    }

}
