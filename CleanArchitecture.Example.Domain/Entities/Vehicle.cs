using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Vehicle : IEntity
    {

        #region - - - - - - Properties - - - - - -

        public string Make { get; set; }

        public string Model { get; set; }

        public Customer Owner { get; set; }

        public string RegistrationNumber { get; set; }

        #endregion Properties

        #region - - - - - - IEntity Implementation - - - - - -

        public EntityID ID { get; set; }

        #endregion IEntity Implementation

    }

}
