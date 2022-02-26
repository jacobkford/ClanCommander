namespace ClanCommander.ApplicationCore.Data.ValueConverters;

internal class ClanIdValueConverter : ValueConverter<ClanId, string>
{
    private static readonly Expression<Func<ClanId, string>> convertToProviderExpression = clanId => clanId.Value;
    private static readonly Expression<Func<string, ClanId>> convertFromProviderExpression = value => ClanId.FromString(value);

    public ClanIdValueConverter(ConverterMappingHints? mappingHints = null)
        : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
    { }
}
