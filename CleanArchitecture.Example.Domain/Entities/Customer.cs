using CleanArchitecture.Services.Entities;
using System.Collections.Generic;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Customer : IEntity
    {

        #region - - - - - - Properties - - - - - -

        public Person CustomerDetails { get; set; }

        public ICollection<Vehicle> Vehicles { get; } = new HashSet<Vehicle>();

        #endregion Properties

        #region - - - - - - IEntity Implementation - - - - - -

        public EntityID ID { get; set; }

        #endregion IEntity Implementation

    }

}
