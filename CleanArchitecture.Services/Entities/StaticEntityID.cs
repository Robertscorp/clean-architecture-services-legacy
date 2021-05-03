namespace CleanArchitecture.Services.Entities
{

    internal class StaticEntityID : EntityID
    {

        #region - - - - - - Constructors - - - - - -

        public StaticEntityID(long value)
            => this.Value = value;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public long Value { get; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public override bool Equals(object obj)
            => ReferenceEquals(obj, this) || (obj is StaticEntityID _EnumerationEntityID && Equals(this.Value, _EnumerationEntityID.Value));

        public override int GetHashCode()
            => this.Value.GetHashCode();

        #endregion Methods

    }

}
