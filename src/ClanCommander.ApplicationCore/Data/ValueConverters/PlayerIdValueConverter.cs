namespace ClanCommander.ApplicationCore.Data.ValueConverters;

internal class PlayerIdValueConverter : ValueConverter<PlayerId, string>
{
    private static readonly Expression<Func<PlayerId, string>> convertToProviderExpression = accountId => accountId.Value;
    private static readonly Expression<Func<string, PlayerId>> convertFromProviderExpression = value => PlayerId.FromString(value);

    public PlayerIdValueConverter(ConverterMappingHints? mappingHints = null)
        : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
    { }
}
