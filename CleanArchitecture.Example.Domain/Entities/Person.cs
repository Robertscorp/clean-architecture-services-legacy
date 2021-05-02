﻿using CleanArchitecture.Example.Domain.Enumerations;
using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Person : IEntity
    {

        #region - - - - - - Properties - - - - - -

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public GenderEnumeration Gender { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        #endregion Properties

        #region - - - - - - IEntity Implementation - - - - - -

        public EntityID ID { get; set; }

        #endregion IEntity Implementation

    }

}
