namespace CleanArchitecture.Example.Domain.Entities
{

    public class VehicleService
    {

        #region - - - - - - Properties - - - - - -

        public Day DayServiced { get; set; }

        public Employee ServicedBy { get; set; }

        public Vehicle Vehicle { get; set; }

        #endregion Properties

    }

}
