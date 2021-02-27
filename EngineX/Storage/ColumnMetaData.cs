namespace EngineX.Storage
{
    public record ColumnMetaData (TableMetaData Table, string ColumnName, string DataType)
    {
    }
}