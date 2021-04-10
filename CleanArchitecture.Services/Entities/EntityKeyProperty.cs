namespace CleanArchitecture.Services.Entities
{

    public class EntityKeyProperty
    {

        #region - - - - - - Properties - - - - - -

        public string Name { get; set; }

        public object Value { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public static EntityKeyProperty GetEntityKeyProperty(string name, object value)
            => new EntityKeyProperty { Name = name, Value = value };

        #endregion Methods

    }

}
