using AutoMapper;
using CleanArchitecture.Example.Domain.Entities;

namespace CleanArchitecture.Example.Application.UseCases.Employees.GetEmployeeRoles
{

    public class GetEmployeeRolesProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public GetEmployeeRolesProfile()
        {
            _ = this.CreateMap<EmployeeRole, EmployeeRoleDto>()
                    .ForMember(dest => dest.EmployeeRoleID, opts => opts.MapFrom(src => src.ID))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.ToString()));
        }

        #endregion Constructors

    }

}
