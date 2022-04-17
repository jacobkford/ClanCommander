namespace ClanCommander.ApplicationCore.Data.ValueConverters;

internal class DiscordUserIdValueConverter : ValueConverter<DiscordUserId, ulong>
{
    private static readonly Expression<Func<DiscordUserId, ulong>> convertToProviderExpression = discordUserId => discordUserId.Value;
    private static readonly Expression<Func<ulong, DiscordUserId>> convertFromProviderExpression = value => DiscordUserId.FromUInt64(value);

    public DiscordUserIdValueConverter(ConverterMappingHints? mappingHints = null)
        : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
    { }
}

internal class DiscordUserIdTypeHandler : SqlMapper.TypeHandler<DiscordUserId>
{
    public override DiscordUserId Parse(object value)
    {
        return DiscordUserId.Parse(value.ToString() ?? "");
    }

    public override void SetValue(IDbDataParameter parameter, DiscordUserId value)
    {
        parameter.DbType = DbType.Int64;
        parameter.Value = value.Value;
    }
}
