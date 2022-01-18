using ABU.Portfolio.Models;

namespace ABU.Portfolio.Services.Abstractions;

public interface IGitHubTableStorageService : ITableStorageService
{
    Task<IEnumerable<GitHubRepositoryEntity>> RetrieveAllAsync(CancellationToken ct = default);
}