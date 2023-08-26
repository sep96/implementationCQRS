using CQRS.Core.Events;
using Employee.Common.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Employee.Query.Infrastructure.Converter
{
    public class EventJsonConverter : JsonConverter<BaseEvent>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(BaseEvent));  
        }
        public override BaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var doc))
                throw new Exception("Faild to Prase");
            if (!doc.RootElement.TryGetProperty("Type", out var type))
                throw new Exception("Could not Detect The Type");
            var TypeDisciminator = type.GetString();
            var json = doc.RootElement.GetRawText();
            return TypeDisciminator switch
            {
                nameof(EmployeeCreatedEvent) => JsonSerializer.Deserialize<EmployeeCreatedEvent>(json, options),
                nameof(AddVacationEvent) => JsonSerializer.Deserialize<AddVacationEvent>(json, options),
                nameof(DayWorkEvent) => JsonSerializer.Deserialize<DayWorkEvent>(json, options),
                nameof(UpdateEmployeeEvent) => JsonSerializer.Deserialize<UpdateEmployeeEvent>(json, options),
                nameof(DeleteEmployeeEvent) => JsonSerializer.Deserialize<DeleteEmployeeEvent>(json, options),
                _=> throw new JsonException("is not supported yet")
            };
        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
