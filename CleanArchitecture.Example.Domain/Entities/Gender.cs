using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Gender : StaticEntity
    {

        #region - - - - - - Fields - - - - - -

        public static readonly Gender Female = new("Female", 1);
        public static readonly Gender Male = new("Male", 2);
        public static readonly Gender Mayonnaise = new("Mayonnaise", 3); // The secret gender.

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        private Gender(string name, int value) : base(name, value) { }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        protected override bool IsContextEntity()
            => !Equals(this, Mayonnaise);

        #endregion Methods

    }

}
