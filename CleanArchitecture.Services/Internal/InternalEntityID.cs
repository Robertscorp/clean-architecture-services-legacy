using CleanArchitecture.Services.Entities;
using System;
using System.Text.Json;

namespace CleanArchitecture.Services.Internal
{

    internal class InternalEntityID : EntityID
    {

        #region - - - - - - Properties - - - - - -

        public InternalEntityIDData Data { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public override bool Equals(object obj)
            => obj is InternalEntityID _InternalEntityID && Equals(_InternalEntityID.Data, this.Data);

        public override int GetHashCode()
            => this.Data?.GetHashCode() ?? this.GetHashCode();

        public override string ToString()
            => this.Data?.ToString() ?? string.Empty;

        #endregion Methods

    }

    internal abstract class InternalEntityIDData
    {

        #region - - - - - - Methods - - - - - -

        public abstract void Write(Utf8JsonWriter writer, JsonSerializerOptions options);

        #endregion Methods

    }

    internal abstract class InternalEntityIDData<TValue> : InternalEntityIDData
    {

        #region - - - - - - Constructors - - - - - -

        public InternalEntityIDData(TValue value)
            => this.Value = value;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public TValue Value { get; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public override string ToString()
            => this.Value.ToString();

        public override void Write(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            if (this.Value == null)
                writer.WriteNullValue();

            else if (this.Value is bool _Bool)
                writer.WriteBooleanValue(_Bool);

            else if (this.Value is Guid _Guid)
                writer.WriteStringValue(_Guid.ToString());

            else if (this.Value is int _Int)
                writer.WriteNumberValue(_Int);

            else if (this.Value is long _Long)
                writer.WriteNumberValue(_Long);

            else if (this.Value is string _String)
                writer.WriteStringValue(_String);

            else if (this.Value is object _)
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            }
        }

        #endregion Methods

    }

    internal class InternalDeserialisedEntityIDData<TValue> : InternalEntityIDData<TValue>
    {

        #region - - - - - - Constructors - - - - - -

        public InternalDeserialisedEntityIDData(TValue value) : base(value) { }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public override bool Equals(object obj)
            => obj is InternalEntityIDData _InternalEntityIDData && Equals(this.GetHashCode(), _InternalEntityIDData.GetHashCode());

        public override int GetHashCode()
            => this.Value?.GetHashCode() ?? default;

        #endregion Methods

    }

    internal class InternalEntityIDData<TEntity, TValue> : InternalEntityIDData<TValue>
    {

        #region - - - - - - Constructors - - - - - -

        public InternalEntityIDData(TValue value) : base(value) { }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public override bool Equals(object obj)
            => ReferenceEquals(this, obj) || obj is InternalDeserialisedEntityIDData<TValue> _DeserialisedEntityIDData && Equals(this.Value, _DeserialisedEntityIDData.Value);

        public override int GetHashCode()
            => this.Value?.GetHashCode() ?? default;

        #endregion Methods

    }

}
