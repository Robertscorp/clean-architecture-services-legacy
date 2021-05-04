using AutoMapper;
using CleanArchitecture.Example.Domain.Entities;

namespace CleanArchitecture.Example.Application.UseCases.People.GetGenders
{

    public class GetGendersProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public GetGendersProfile()
        {
            _ = this.CreateMap<Gender, GenderDto>()
                    .ForMember(dest => dest.GenderID, opts => opts.MapFrom(src => src.ID))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.ToString()));
        }

        #endregion Constructors

    }

}
