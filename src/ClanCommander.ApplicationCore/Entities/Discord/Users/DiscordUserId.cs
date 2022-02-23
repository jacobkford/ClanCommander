namespace ClanCommander.ApplicationCore.Entities.Discord.Users;

[System.ComponentModel.TypeConverter(typeof(UserIdTypeConverter))]
[System.Text.Json.Serialization.JsonConverter(typeof(UserIdJsonConverter))]
[Newtonsoft.Json.JsonConverter(typeof(UserIdNewtonsoftJsonConverter))]
internal partial struct DiscordUserId : IEquatable<DiscordUserId>
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
    private DiscordUserId(ulong value)
    {
        _value = value;
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static DiscordUserId FromUInt64(ulong value)
    {
        return new DiscordUserId(value);
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public override string ToString()
    {
        return "UserId { Value = " + ValueAsString + " }";
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public bool Equals(DiscordUserId other)
    {
        return Value == other.Value;
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public override bool Equals(object? other)
    {
        if (other is DiscordUserId)
        {
            return Equals((DiscordUserId)other);
        }
        else
        {
            return false;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static bool operator ==(DiscordUserId a, DiscordUserId b)
    {
        return EqualityComparer<DiscordUserId>.Default.Equals(a, b);
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static bool operator !=(DiscordUserId a, DiscordUserId b)
    {
        return !(a == b);
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static DiscordUserId Parse(string value)
    {
        DiscordUserId result;
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
        out DiscordUserId result)
    {
        ulong id;
        if (ulong.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out id))
        {
            result = new DiscordUserId(id);
            return true;
        }
        else
        {
            result = default;
            return false;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
    public static DiscordUserId Parse(ReadOnlySpan<char> value)
    {
        DiscordUserId result;
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
        out DiscordUserId result)
    {
        ulong id;
        if (ulong.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out id))
        {
            result = new DiscordUserId(id);
            return true;
        }
        else
        {
            result = default;
            return false;
        }
    }

    private partial class UserIdTypeConverter : System.ComponentModel.TypeConverter
    {
        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(ulong) || sourceType == typeof(DiscordUserId);
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
                        throw new ArgumentException("Cannot convert '" + value + "' to UserId");
                    }
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Type? destinationType)
        {
            return destinationType == typeof(ulong) || destinationType == typeof(DiscordUserId) || destinationType == typeof(string);
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, System.Type destinationType)
        {
            if (value != null)
            {
                if (destinationType == typeof(string))
                {
                    return ((DiscordUserId)value).ValueAsString;
                }

                if (destinationType == typeof(DiscordUserId))
                {
                    return value;
                }

                if (destinationType == typeof(ulong))
                {
                    return ((DiscordUserId)value).Value;
                }
            }

            throw new ArgumentException("Cannot convert '" + value + "' to '" + destinationType + "'");
        }
    }

    private partial class UserIdJsonConverter : System.Text.Json.Serialization.JsonConverter<DiscordUserId>
    {
        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override void Write(System.Text.Json.Utf8JsonWriter writer, DiscordUserId value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override DiscordUserId Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            DiscordUserId value = default;
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
                        value = new DiscordUserId(reader.GetUInt64());
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
                value = new DiscordUserId(reader.GetUInt64());
#nullable enable
            }

            return value;
        }
    }

    private partial class UserIdNewtonsoftJsonConverter : Newtonsoft.Json.JsonConverter
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
            return type == typeof(DiscordUserId);
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
                writer.WriteValue(((DiscordUserId)value).Value);
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("Meziantou.Framework.StronglyTypedId", "1.0.17.0")]
        public override object? ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            DiscordUserId value = default;
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
                        value = new DiscordUserId(serializer.Deserialize<ulong>(reader));
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
                value = new DiscordUserId(serializer.Deserialize<ulong>(reader));
#nullable enable
            }

            return value;
        }
    }
}
