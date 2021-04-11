using AutoMapper;
using CleanArchitecture.Example.Domain.Enumerations;

namespace CleanArchitecture.Example.Application.UseCases.Employees.GetEmployeeRoles
{

    public class GetEmployeeRolesProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public GetEmployeeRolesProfile()
        {
            _ = this.CreateMap<EmployeeRoleEnumeration, EmployeeRoleDto>()
                    .ForMember(dest => dest.EmployeeRoleID, opts => opts.MapFrom(src => src.ToEntityID()))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.ToString()));
        }

        #endregion Constructors

    }

}
