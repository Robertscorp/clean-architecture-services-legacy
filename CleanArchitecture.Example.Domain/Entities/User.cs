using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class User : IEntity
    {

        #region - - - - - - Properties - - - - - -

        public string HashedPassword { get; set; }

        public string UserName { get; set; }

        #endregion Properties

        #region - - - - - - IEntity Implementation - - - - - -

        public EntityID ID { get; }

        #endregion IEntity Implementation

    }

}
