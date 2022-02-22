namespace ClanCommander.ApplicationCore.Entities.Shared;

[System.ComponentModel.TypeConverter(typeof(PlayerIdTypeConverter))]
[System.Text.Json.Serialization.JsonConverter(typeof(PlayerIdJsonConverter))]
[Newtonsoft.Json.JsonConverter(typeof(PlayerIdNewtonsoftJsonConverter))]
partial struct ClashOfClansPlayerId : IEquatable<ClashOfClansPlayerId>
{
    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    private readonly string _value;

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public string Value
    {
        get
        {
            return _value;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public string ValueAsString
    {
        get
        {
            return Value;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    private ClashOfClansPlayerId(string value)
    {
        _value = value;
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static ClashOfClansPlayerId FromString(string value)
    {
        return new ClashOfClansPlayerId(value);
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public override string ToString()
    {
        if (Value == null)
        {
            return "PlayerId { Value = <null> }";
        }
        else
        {
            return "PlayerId { Value = " + ValueAsString + " }";
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public override int GetHashCode()
    {
        if (Value == null)
        {
            return 0;
        }
        else
        {
            return Value.GetHashCode();
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public bool Equals(ClashOfClansPlayerId other)
    {
        return Value == other.Value;
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public override bool Equals(object? other)
    {
        if (other is ClashOfClansPlayerId)
        {
            return Equals((ClashOfClansPlayerId)other);
        }
        else
        {
            return false;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static bool operator ==(ClashOfClansPlayerId a, ClashOfClansPlayerId b)
    {
        return EqualityComparer<ClashOfClansPlayerId>.Default.Equals(a, b);
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static bool operator !=(ClashOfClansPlayerId a, ClashOfClansPlayerId b)
    {
        return !(a == b);
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static ClashOfClansPlayerId Parse(string value)
    {
        ClashOfClansPlayerId result;
        if (TryParse(value, out result))
        {
            return result;
        }
        else
        {
            throw new FormatException("Value '" + value.ToString() + "' is not valid");
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static bool TryParse(string value, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]
        out ClashOfClansPlayerId result)
    {
        result = new ClashOfClansPlayerId(value);
        return true;
    }

    private partial class PlayerIdTypeConverter : System.ComponentModel.TypeConverter
    {
        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(string) || sourceType == typeof(ClashOfClansPlayerId);
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override object? ConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
        {
            if (value == null)
            {
                return default(string);
            }
            else
            {
                if (value is string)
                {
                    return FromString((string)value);
                }
                else
                {
                    if (value is string)
                    {
                        return Parse((string)value);
                    }
                    else
                    {
                        throw new ArgumentException("Cannot convert '" + value + "' to PlayerId");
                    }
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Type? destinationType)
        {
            return destinationType == typeof(string) || destinationType == typeof(ClashOfClansPlayerId) || destinationType == typeof(string);
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, System.Type destinationType)
        {
            if (value != null)
            {
                if (destinationType == typeof(string))
                {
                    return ((ClashOfClansPlayerId)value).ValueAsString;
                }

                if (destinationType == typeof(ClashOfClansPlayerId))
                {
                    return value;
                }

                if (destinationType == typeof(string))
                {
                    return ((ClashOfClansPlayerId)value).Value;
                }
            }

            throw new ArgumentException("Cannot convert '" + value + "' to '" + destinationType + "'");
        }
    }

    private partial class PlayerIdJsonConverter : System.Text.Json.Serialization.JsonConverter<ClashOfClansPlayerId>
    {
        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override void Write(System.Text.Json.Utf8JsonWriter writer, ClashOfClansPlayerId value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override ClashOfClansPlayerId Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            var value = default(ClashOfClansPlayerId);
            if (reader.TokenType == System.Text.Json.JsonTokenType.StartObject)
            {
                bool valueRead = false;
                reader.Read();
                while (reader.TokenType != System.Text.Json.JsonTokenType.EndObject)
                {
                    if (!valueRead && reader.TokenType == System.Text.Json.JsonTokenType.PropertyName && reader.ValueTextEquals("Value"))
                    {
                        reader.Read();
#nullable disable
                        value = new ClashOfClansPlayerId(reader.GetString());
#nullable enable
                        valueRead = true;
                        reader.Read();
                    }
                    else
                    {
                        reader.Skip();
                        reader.Read();
                    }
                }
            }
            else
            {
#nullable disable
                value = new ClashOfClansPlayerId(reader.GetString());
#nullable enable
            }

            return value;
        }
    }

    private partial class PlayerIdNewtonsoftJsonConverter : Newtonsoft.Json.JsonConverter
    {
        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override bool CanConvert(System.Type type)
        {
            return type == typeof(ClashOfClansPlayerId);
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object? value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(((ClashOfClansPlayerId)value).Value);
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override object? ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var value = default(ClashOfClansPlayerId);
            if (reader.TokenType == Newtonsoft.Json.JsonToken.StartObject)
            {
                bool valueRead = false;
                reader.Read();
                while (reader.TokenType != Newtonsoft.Json.JsonToken.EndObject)
                {
                    if (!valueRead && reader.TokenType == Newtonsoft.Json.JsonToken.PropertyName && (string?)reader.Value == "Value")
                    {
                        reader.Read();
#nullable disable
                        value = new ClashOfClansPlayerId(serializer.Deserialize<string>(reader));
#nullable enable
                        valueRead = true;
                        reader.Read();
                    }
                    else
                    {
                        reader.Skip();
                        reader.Read();
                    }
                }
            }
            else
            {
#nullable disable
                value = new ClashOfClansPlayerId(serializer.Deserialize<string>(reader));
#nullable enable
            }

            return value;
        }
    }
}
