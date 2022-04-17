namespace ClanCommander.ApplicationCore.Data.ValueConverters;

internal class DiscordGuildIdValueConverter : ValueConverter<DiscordGuildId, ulong>
{
    private static readonly Expression<Func<DiscordGuildId, ulong>> convertToProviderExpression = discordGuildId => discordGuildId.Value;
    private static readonly Expression<Func<ulong, DiscordGuildId>> convertFromProviderExpression = value => DiscordGuildId.FromUInt64(value);

    public DiscordGuildIdValueConverter(ConverterMappingHints? mappingHints = null) 
        : base(convertToProviderExpression, convertFromProviderExpression, mappingHints) 
    { }
}

internal class DiscordGuildIdTypeHandler : SqlMapper.TypeHandler<DiscordGuildId>
{
    public override DiscordGuildId Parse(object value)
    {
        return DiscordGuildId.Parse(value.ToString() ?? "");
    }

    public override void SetValue(IDbDataParameter parameter, DiscordGuildId value)
    {
        parameter.DbType = DbType.Int64;
        parameter.Value = value.Value;
    }
}