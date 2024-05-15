using System.Text.Json;
using System.Text.Json.Serialization;

namespace IriTools;

public class IriReferenceConverter : JsonConverter<IriReference>
{
    public override IriReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string uriString = reader.GetString();
        return new IriReference(uriString);
    }

    public override void Write(Utf8JsonWriter writer, IriReference value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(IriReference);
    }
}