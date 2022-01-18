using ABU.Portfolio.Models;
using ABU.Portfolio.Services.Abstractions;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace ABU.Portfolio.Services.Implementations;

public class GitHubTableStorageService : TableStorageService, IGitHubTableStorageService
{
    private const string TableName = "repos";
    private readonly ITableStorageClient _client;

    public GitHubTableStorageService(ITableStorageClient client) 
        : base(client)
    {
        _client = Guard.Against.Null(client, nameof(client));
    }

    public async Task<IEnumerable<GitHubRepositoryEntity>> RetrieveAllAsync(CancellationToken ct = default)
    {
        var table = await _client.GetCloudTableAsync(TableName, ct);
        var query = 
            from entity in table.CreateQuery<GitHubRepositoryEntity>() 
            select entity;

        return query.ToList();
    }
}