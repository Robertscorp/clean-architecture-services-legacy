using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Internal;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArchitecture.Services.Infrastructure
{

    public class EntityIDConverter : JsonConverter<EntityID>
    {

        #region - - - - - - Methods - - - - - -

        public override EntityID Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => new InternalEntityID
            {
                Data = reader.TokenType switch
                {
                    JsonTokenType.Comment => new InternalDeserialisedEntityIDData<string>(reader.GetComment()),
                    JsonTokenType.False => new InternalDeserialisedEntityIDData<bool>(false),
                    JsonTokenType.Null => new InternalDeserialisedEntityIDData<object>(null),
                    JsonTokenType.Number => new InternalDeserialisedEntityIDData<long>(reader.GetInt64()),
                    JsonTokenType.String => new InternalDeserialisedEntityIDData<string>(reader.GetString()),
                    JsonTokenType.True => new InternalDeserialisedEntityIDData<bool>(true),
                    _ => new InternalDeserialisedEntityIDData<object>(null)
                }
            };

        public override void Write(Utf8JsonWriter writer, EntityID value, JsonSerializerOptions options)
            => (value as InternalEntityID)?.Data?.Write(writer, options);

        #endregion Methods

    }

}
