using CleanArchitecture.Services.Entities;
using System;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Day : IEntity
    {

        #region - - - - - - Properties - - - - - -

        public DateTime Date { get; set; }

        #endregion Properties

        #region - - - - - - IEntity Implementation - - - - - -

        public EntityID ID { get; }

        #endregion IEntity Implementation

    }

}
