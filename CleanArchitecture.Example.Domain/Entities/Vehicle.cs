namespace CleanArchitecture.Example.Domain.Entities
{

    public class Vehicle
    {

        #region - - - - - - Properties - - - - - -

        public string Make { get; set; }

        public string Model { get; set; }

        public Customer Owner { get; set; }

        public string RegistrationNumber { get; set; }

        #endregion Properties

    }

}
