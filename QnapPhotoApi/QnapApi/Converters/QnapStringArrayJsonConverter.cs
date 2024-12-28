namespace Ragware.QnapPhotoApi.QnapApi.Converters;

using System.Text.Json;
using System.Text.Json.Serialization;

internal class QnapStringArrayJsonConverter : JsonConverter<string[]>
{
    public override string[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (reader.GetString() ?? string.Empty).Split(";", StringSplitOptions.RemoveEmptyEntries);
    }

    public override void Write(Utf8JsonWriter writer, string[] value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(string.Join(";", value));
    }
}