using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.InterfaceAdapters.ViewModels.Genders
{

    public class ExistingGenderViewModel
    {

        #region - - - - - - Constructors - - - - - -

        public ExistingGenderViewModel(EntityID genderID, string name)
        {
            this.GenderID = genderID;
            this.Name = name;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public EntityID GenderID { get; private set; }

        public string Name { get; private set; }

        #endregion Properties

    }

}
