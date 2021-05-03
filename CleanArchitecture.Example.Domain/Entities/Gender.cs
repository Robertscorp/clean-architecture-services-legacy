using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Gender : StaticEntity
    {

        #region - - - - - - Fields - - - - - -

        public static readonly Gender Female = new Gender("Female", 1);
        public static readonly Gender Male = new Gender("Male", 2);
        public static readonly Gender Mayonnaise = new Gender("Mayonnaise", 3); // The secret gender.

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        private Gender(string name, int value) : base(name, value) { }

        #endregion Constructors

    }

}
