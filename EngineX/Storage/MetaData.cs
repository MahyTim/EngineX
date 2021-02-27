using System;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace EngineX.Storage
{
    public class MetaData : IDisposable
    {
        private readonly SqlConnection _connection;

        public MetaData(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public TableMetaData[] Tables()
        {
            _connection.Open();
            var table = _connection.GetSchema("Tables");
            _connection.Close();
            var selectedRows =
                from info in table.AsEnumerable()
                select new TableMetaData(info["TABLE_SCHEMA"].ToString(), info["TABLE_NAME"].ToString());
            return selectedRows.ToArray();
        }

        public ColumnMetaData[] Columns(TableMetaData filter)
        {
            _connection.Open();
            var table = _connection.GetSchema("Columns");
            _connection.Close();
            var selectedRows =
                from info in table.AsEnumerable()
                select new ColumnMetaData(
                    new TableMetaData(info["TABLE_SCHEMA"].ToString(), info["TABLE_NAME"].ToString())
                    , info["COLUMN_NAME"].ToString()
                    , info["DATA_TYPE"].ToString());
            return selectedRows.Where(z => z.Table.Equals(filter)).ToArray();
        }

        public IndexColumnMetaData[] IndexColumns()
        {
            _connection.Open();
            var table = _connection.GetSchema("IndexColumns");
            _connection.Close();
            var selectedRows =
                from info in table.AsEnumerable()
                select new IndexColumnMetaData(
                    new TableMetaData(info["table_schema"].ToString(), info["table_name"].ToString()),
                    info["column_name"].ToString(),
                    info["constraint_schema"].ToString(),
                    info["constraint_name"].ToString(),
                    info["KeyType"].ToString());

            return selectedRows.ToArray();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}