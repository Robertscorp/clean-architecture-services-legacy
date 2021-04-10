using System.Collections.Generic;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Customer
    {

        #region - - - - - - Properties - - - - - -

        public Person CustomerDetails { get; set; }

        public ICollection<Vehicle> Vehicles { get; } = new HashSet<Vehicle>();

        #endregion Properties

    }

}
