using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Azure.Cli.ContextSwitcher.Model.Serialization
{
    internal class LoginTypeConverter : JsonConverter<LoginType>
    {
        public override LoginType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return (LoginType)Enum.Parse(typeof(LoginType), reader.GetString(), true);
        }

        public override void Write(Utf8JsonWriter writer, LoginType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
