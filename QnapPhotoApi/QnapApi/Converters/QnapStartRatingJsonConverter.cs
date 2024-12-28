namespace Ragware.QnapPhotoApi.QnapApi.Converters;

using System.Text.Json;
using System.Text.Json.Serialization;

public class QnapStartRatingJsonConverter : JsonConverter<StarRating?>
{
    public override StarRating? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "20" => StarRating.One,
            "40" => StarRating.Two,
            "60" => StarRating.Three,
            "80" => StarRating.Four,
            "100" => StarRating.Five,
            _ => null
        };
    }

    public override void Write(Utf8JsonWriter writer, StarRating? value, JsonSerializerOptions options)
    {
        string? stringValue = value switch
        {
            StarRating.One => "20",
            StarRating.Two => "40",
            StarRating.Three => "60",
            StarRating.Four => "80",
            StarRating.Five => "100",
            _ => null
        };
        writer.WriteStringValue(stringValue);
    }
}