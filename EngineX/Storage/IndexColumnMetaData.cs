namespace EngineX.Storage
{
    public record IndexColumnMetaData(TableMetaData Table, string ColumnName, string ConstraintSchema,
        string ConstraintName, string KeyType)
    {
    }
}