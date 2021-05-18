using AutoMapper;
using CleanArchitecture.Example.Application.UseCases.People.GetGenders;
using CleanArchitecture.Example.InterfaceAdapters.ViewModels.Genders;

namespace CleanArchitecture.Example.InterfaceAdapters.Infrastructure.Mappings
{

    public class GenderMappingProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public GenderMappingProfile()
            => _ = this.CreateMap<GenderDto, ExistingGenderViewModel>();

        #endregion Constructors

    }

}
