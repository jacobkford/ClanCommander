namespace ClanCommander.ApplicationCore.Data.ValueConverters;

internal class ClanIdValueConverter : ValueConverter<ClanId, string>
{
    private static readonly Expression<Func<ClanId, string>> convertToProviderExpression = clanId => clanId.Value;
    private static readonly Expression<Func<string, ClanId>> convertFromProviderExpression = value => ClanId.FromString(value);

    public ClanIdValueConverter(ConverterMappingHints? mappingHints = null)
        : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
    { }
}

internal class ClanIdTypeHandler : SqlMapper.TypeHandler<ClanId>
{
    public override ClanId Parse(object value)
    {
        return ClanId.FromString((string)value);
    }

    public override void SetValue(IDbDataParameter parameter, ClanId value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value.Value;
    }
}
