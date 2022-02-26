namespace ClanCommander.ApplicationCore.Data.ValueConverters;
internal class ClanMemberRoleValueConverter : ValueConverter<ClanMemberRole, int>
{
    private static readonly Expression<Func<ClanMemberRole, int>> convertToProviderExpression = role => role.Value;
    private static readonly Expression<Func<int, ClanMemberRole>> convertFromProviderExpression = value => ClanMemberRole.FromValue(value);

    public ClanMemberRoleValueConverter(ConverterMappingHints? mappingHints = null)
        : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
    { }
}
