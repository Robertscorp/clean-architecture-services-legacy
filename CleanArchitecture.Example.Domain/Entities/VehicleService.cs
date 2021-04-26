using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class VehicleService
    {

        #region - - - - - - Properties - - - - - -

        public EntityID ID { get; set; }

        public Day DayServiced { get; set; }

        public Employee ServicedBy { get; set; }

        public Vehicle Vehicle { get; set; }

        #endregion Properties

    }

}
