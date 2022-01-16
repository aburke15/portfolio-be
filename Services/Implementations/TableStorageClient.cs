using System.Runtime.InteropServices.ComTypes;
using ABU.Portfolio.Services.Abstractions;
using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Services.Implementations;

public class TableStorageClient : ITableStorageClient
{
    private const string AzureConnectionString = "DOTNET_AZURE_CONNECTION_STRING";
    private readonly CloudTableClient _client;
    
    public TableStorageClient()
    {
        var storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable(AzureConnectionString));
        _client = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
    }
    
    public async Task<object> ExecuteTableOperationAsync(TableOperation operation, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<CloudTable> GetCloudTableAsync(string table, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}