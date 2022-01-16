using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Services.Abstractions;

public interface ITableStorageClient
{
    Task<object> ExecuteTableOperationAsync(TableOperation operation, CancellationToken ct = default);
    Task<CloudTable> GetCloudTableAsync(string table, CancellationToken ct = default);
}