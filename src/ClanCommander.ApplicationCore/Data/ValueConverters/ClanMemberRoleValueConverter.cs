namespace ClanCommander.ApplicationCore.Data.ValueConverters;

internal class ClanMemberRoleValueConverter : ValueConverter<ClanMemberRole, int>
{
    private static readonly Expression<Func<ClanMemberRole, int>> convertToProviderExpression = role => role.Value;
    private static readonly Expression<Func<int, ClanMemberRole>> convertFromProviderExpression = value => ClanMemberRole.FromValue(value);

    public ClanMemberRoleValueConverter(ConverterMappingHints? mappingHints = null)
        : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
    { }
}

internal class ClanMemberRoleTypeHandler : SqlMapper.TypeHandler<ClanMemberRole>
{
    public override ClanMemberRole Parse(object value)
    {
        if (value.GetType() == typeof(string))
            return ClanMemberRole.FromName((string)value);

        return ClanMemberRole.FromValue((int)value);
    }

    public override void SetValue(IDbDataParameter parameter, ClanMemberRole value)
    {
        parameter.DbType = DbType.Int32;
        parameter.Value = value.Value;
    }
}