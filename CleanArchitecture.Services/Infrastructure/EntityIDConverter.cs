using CleanArchitecture.Services.Entities;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArchitecture.Services.Infrastructure
{

    public class EntityIDConverter : JsonConverter<EntityID>
    {

        #region - - - - - - Methods - - - - - -

        public override EntityID Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => null;

        public override void Write(Utf8JsonWriter writer, EntityID value, JsonSerializerOptions options)
        {
            if (value is StaticEntityID _StaticEntityID)
            {
                writer.WriteNumberValue(_StaticEntityID.Value);
                return;
            }
        }

        #endregion Methods

    }

}
