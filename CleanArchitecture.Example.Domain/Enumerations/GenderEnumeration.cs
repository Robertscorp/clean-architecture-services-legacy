namespace CleanArchitecture.Example.Domain.Enumerations
{

    public class GenderEnumeration : Enumeration
    {

        #region - - - - - - Fields - - - - - -

        public static readonly GenderEnumeration Female = new GenderEnumeration("Female", 1);
        public static readonly GenderEnumeration Male = new GenderEnumeration("Male", 2);
        public static readonly GenderEnumeration Mayonnaise = new GenderEnumeration("Mayonnaise", 3); // The secret gender.

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        private GenderEnumeration(string name, int value) : base(name, value) { }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public static implicit operator GenderEnumeration(int value)
            => Get<GenderEnumeration>(value);

        #endregion Methods

    }

}
