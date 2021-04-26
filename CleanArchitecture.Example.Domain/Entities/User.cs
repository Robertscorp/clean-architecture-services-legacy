using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class User
    {

        #region - - - - - - Properties - - - - - -

        public EntityID ID { get; set; }

        public string HashedPassword { get; set; }

        public string UserName { get; set; }

        #endregion Properties

    }

}
