using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class VehicleService : IEntity
    {

        #region - - - - - - Properties - - - - - -

        public Day DayServiced { get; set; }

        public Employee ServicedBy { get; set; }

        public Vehicle Vehicle { get; set; }

        #endregion Properties

        #region - - - - - - IEntity Implementation - - - - - -

        public EntityID ID { get; }

        #endregion IEntity Implementation

    }

}
