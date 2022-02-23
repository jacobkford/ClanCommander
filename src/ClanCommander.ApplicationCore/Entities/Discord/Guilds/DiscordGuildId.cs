namespace ClanCommander.ApplicationCore.Entities.Discord.Guilds;

[System.ComponentModel.TypeConverter(typeof(DiscordServerIdTypeConverter))]
[System.Text.Json.Serialization.JsonConverter(typeof(DiscordServerIdJsonConverter))]
[Newtonsoft.Json.JsonConverter(typeof(DiscordServerIdNewtonsoftJsonConverter))]
internal partial struct DiscordGuildId : IEquatable<DiscordGuildId>
{
    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    private readonly ulong _value;

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public ulong Value
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
            return Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    private DiscordGuildId(ulong value)
    {
        _value = value;
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static DiscordGuildId FromUInt64(ulong value)
    {
        return new DiscordGuildId(value);
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public override string ToString()
    {
        return "DiscordServerId { Value = " + ValueAsString + " }";
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public bool Equals(DiscordGuildId other)
    {
        return Value == other.Value;
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public override bool Equals(object? other)
    {
        if (other is DiscordGuildId)
        {
            return Equals((DiscordGuildId)other);
        }
        else
        {
            return false;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static bool operator ==(DiscordGuildId a, DiscordGuildId b)
    {
        return EqualityComparer<DiscordGuildId>.Default.Equals(a, b);
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static bool operator !=(DiscordGuildId a, DiscordGuildId b)
    {
        return !(a == b);
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static DiscordGuildId Parse(string value)
    {
        DiscordGuildId result;
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
        out DiscordGuildId result)
    {
        ulong id;
        if (ulong.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out id))
        {
            result = new DiscordGuildId(id);
            return true;
        }
        else
        {
            result = default;
            return false;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static DiscordGuildId Parse(ReadOnlySpan<char> value)
    {
        DiscordGuildId result;
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
    public static bool TryParse(ReadOnlySpan<char> value, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]
        out DiscordGuildId result)
    {
        ulong id;
        if (ulong.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out id))
        {
            result = new DiscordGuildId(id);
            return true;
        }
        else
        {
            result = default;
            return false;
        }
    }

    private partial class DiscordServerIdTypeConverter : System.ComponentModel.TypeConverter
    {
        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(ulong) || sourceType == typeof(DiscordGuildId);
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override object? ConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
        {
            if (value == null)
            {
                return default(ulong);
            }
            else
            {
                if (value is ulong)
                {
                    return FromUInt64((ulong)value);
                }
                else
                {
                    if (value is string)
                    {
                        return Parse((string)value);
                    }
                    else
                    {
                        throw new ArgumentException("Cannot convert '" + value + "' to DiscordServerId");
                    }
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Type? destinationType)
        {
            return destinationType == typeof(ulong) || destinationType == typeof(DiscordGuildId) || destinationType == typeof(string);
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, System.Type destinationType)
        {
            if (value != null)
            {
                if (destinationType == typeof(string))
                {
                    return ((DiscordGuildId)value).ValueAsString;
                }

                if (destinationType == typeof(DiscordGuildId))
                {
                    return value;
                }

                if (destinationType == typeof(ulong))
                {
                    return ((DiscordGuildId)value).Value;
                }
            }

            throw new ArgumentException("Cannot convert '" + value + "' to '" + destinationType + "'");
        }
    }

    private partial class DiscordServerIdJsonConverter : System.Text.Json.Serialization.JsonConverter<DiscordGuildId>
    {
        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override void Write(System.Text.Json.Utf8JsonWriter writer, DiscordGuildId value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override DiscordGuildId Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            DiscordGuildId value = default;
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
                        value = new DiscordGuildId(reader.GetUInt64());
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
                value = new DiscordGuildId(reader.GetUInt64());
#nullable enable
            }

            return value;
        }
    }

    private partial class DiscordServerIdNewtonsoftJsonConverter : Newtonsoft.Json.JsonConverter
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
            return type == typeof(DiscordGuildId);
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
                writer.WriteValue(((DiscordGuildId)value).Value);
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override object? ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            DiscordGuildId value = default;
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
                        value = new DiscordGuildId(serializer.Deserialize<ulong>(reader));
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
                value = new DiscordGuildId(serializer.Deserialize<ulong>(reader));
#nullable enable
            }

            return value;
        }
    }
}
