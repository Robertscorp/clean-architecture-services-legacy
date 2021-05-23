using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.Validation;
using Moq;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Example.Application.Tests.Support
{

    public class TestEntityIDValidatorFactory : IEntityIDValidatorFactory
    {

        #region - - - - - - Fields - - - - - -

        private readonly Dictionary<Type, EntityID> m_ExistingEntityIDByType = new();

        #endregion Fields

        #region - - - - - - IEntityIDValidatorFactory Implementation - - - - - -

        EntityIDValidator<TEntity> IEntityIDValidatorFactory.GetValidator<TEntity>()
        {
            var _EntityID = this.GetExistingEntityID<TEntity>();
            var _Validator = new Mock<EntityIDValidator<TEntity>>();
            _ = _Validator
                    .Setup(mock => mock.IsValidEntityID(It.IsAny<EntityID>()))
                    .Returns((EntityID id) => Equals(_EntityID, id));

            return _Validator.Object;
        }

        #endregion IEntityIDValidatorFactory Implementation

        #region - - - - - - Methods - - - - - -

        public EntityID GetExistingEntityID<TEntity>()
        {
            if (!this.m_ExistingEntityIDByType.TryGetValue(typeof(TEntity), out var _EntityID))
            {
                _EntityID = new Mock<EntityID>().Object;
                this.m_ExistingEntityIDByType.Add(typeof(TEntity), _EntityID);
            }

            return _EntityID;
        }

        #endregion Methods

    }

}
