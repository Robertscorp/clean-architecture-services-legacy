using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.Extensions;
using CleanArchitecture.Example.Domain.Entities;

namespace CleanArchitecture.Example.Application.Infrastructure.Mapping
{

    public class DtoMappingProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public DtoMappingProfile()
            => _ = this.CreateMap<Employee, UserDto>()
                    .ForMember(dest => dest.EmployeeID, opts => opts.MapFromEntity(src => src))
                    .ForMember(dest => dest.EmployeeRoleID, opts => opts.MapFromEnumeration(src => src.Role))
                    .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.EmployeeDetails.FirstName))
                    .ForMember(dest => dest.GenderID, opts => opts.MapFromEnumeration(src => src.EmployeeDetails.Gender))
                    .ForMember(dest => dest.HashedPassword, opts => opts.MapFrom(src => src.User.HashedPassword))
                    .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.EmployeeDetails.LastName))
                    .ForMember(dest => dest.PersonID, opts => opts.MapFromEntity(src => src.EmployeeDetails))
                    .ForMember(dest => dest.UserID, opts => opts.MapFromEntity(src => src.User))
                    .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.UserName));

        #endregion Constructors

    }

}
