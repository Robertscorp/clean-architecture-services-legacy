using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Application.UseCases.People.GetGenders
{

    public class GenderDto
    {

        #region - - - - - - Properties - - - - - -

        public EntityID GenderID { get; set; }

        public string Name { get; set; }

        #endregion Properties

    }

}
