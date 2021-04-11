using AutoMapper;
using CleanArchitecture.Example.Domain.Enumerations;

namespace CleanArchitecture.Example.Application.UseCases.People.GetGenders
{

    public class GetGendersProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public GetGendersProfile()
        {
            _ = this.CreateMap<GenderEnumeration, GenderDto>()
                    .ForMember(dest => dest.GenderID, opts => opts.MapFrom(src => src.ToEntityID()))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.ToString()));
        }

        #endregion Constructors

    }

}
