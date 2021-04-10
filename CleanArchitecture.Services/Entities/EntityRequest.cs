using System.Collections.Generic;

namespace CleanArchitecture.Services.Entities
{

    public class EntityRequest
    {

        #region - - - - - - Properties - - - - - -

        public string EntityName { get; set; }

        public IEnumerable<EntityKeyProperty> Keys { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public static EntityRequest GetEntityRequest(string propertyName, object value)
            => GetEntityRequest(null, propertyName, value);

        public static EntityRequest GetEntityRequest(string entityName, string propertyName, object value)
            => new EntityRequest() { EntityName = entityName, Keys = new[] { EntityKeyProperty.GetEntityKeyProperty(propertyName, value) } };

        #endregion Methods

    }

}
