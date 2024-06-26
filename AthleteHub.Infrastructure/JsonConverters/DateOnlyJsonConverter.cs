using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resturants.Infrastructure.CustomJsonConverters;

public class DateOnlyJsonConverter:JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return DateOnly.ParseExact(reader.GetString(), Format, CultureInfo.InvariantCulture);
        }
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var dateElement = doc.RootElement;
            int year = dateElement.GetProperty("year").GetInt32();
            int month = dateElement.GetProperty("month").GetInt32();
            int day = dateElement.GetProperty("day").GetInt32();
            return new DateOnly(year, month, day);
        }
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}
