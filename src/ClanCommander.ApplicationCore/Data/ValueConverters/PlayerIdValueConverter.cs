namespace ClanCommander.ApplicationCore.Data.ValueConverters;

internal class PlayerIdValueConverter : ValueConverter<PlayerId, string>
{
    private static readonly Expression<Func<PlayerId, string>> convertToProviderExpression = accountId => accountId.Value;
    private static readonly Expression<Func<string, PlayerId>> convertFromProviderExpression = value => PlayerId.FromString(value);

    public PlayerIdValueConverter(ConverterMappingHints? mappingHints = null)
        : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
    { }
}

internal class PlayerIdTypeHandler : SqlMapper.TypeHandler<PlayerId>
{
    public override PlayerId Parse(object value)
    {
        return PlayerId.FromString((string)value);
    }

    public override void SetValue(IDbDataParameter parameter, PlayerId value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value.Value;
    }
}