using HubSpot.Api.Models.Crm;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HubSpot.Api.Converters;

public class AssociationTypeConverter : JsonConverter<AssociationType>
{

	public override AssociationType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var value = reader.GetString();

		// Get all enum values
		var enumType = typeof(AssociationType);
		foreach (var field in enumType.GetFields())
		{
			// Skip non-enum fields
			if (!field.IsStatic)
			{
				continue;
			}

			// Get the JsonPropertyName attribute, if it exists
			var jsonPropertyNameAttribute = field.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
				.Cast<JsonPropertyNameAttribute>()
				.FirstOrDefault();

			// If the attribute's name matches the input value, return the corresponding enum value
			if (jsonPropertyNameAttribute?.Name == value)
			{
				return (AssociationType)field.GetValue(null)!;
			}
		}

		// If no match is found, throw an exception
		throw new JsonException($"Unknown value: {value}. Update {nameof(AssociationTypeConverter)}.cs");
	}

	public override void Write(Utf8JsonWriter writer, AssociationType value, JsonSerializerOptions options)
	{
		// Get the type of the enum
		var type = value.GetType();

		// Get the member info for the enum value
		var memberInfo = type.GetMember(value.ToString()).FirstOrDefault();

		// Get the JsonPropertyName attribute, if it exists
		var jsonPropertyNameAttribute = memberInfo?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
			.Cast<JsonPropertyNameAttribute>()
			.FirstOrDefault();

		// Use the value from the attribute, or fall back to the enum name
		var stringValue = jsonPropertyNameAttribute?.Name ?? value.ToString();

		writer.WriteStringValue(stringValue);
	}
}