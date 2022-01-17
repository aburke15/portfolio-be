using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Services.Abstractions;

public interface ITableStorageClient
{
    Task<ITableEntity?> ExecuteTableOperationAsync(string tableName, TableOperation operation, CancellationToken ct = default);
    Task<IEnumerable<ITableEntity?>> ExecuteTableBatchOperationAsync(string tableName, TableBatchOperation batchOperation, CancellationToken ct = default);
    Task<CloudTable> GetCloudTableAsync(string tableName, CancellationToken ct = default);
}