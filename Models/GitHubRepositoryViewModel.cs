using Newtonsoft.Json;

namespace ABU.Portfolio.Models;

public record GitHubRepositoryViewModel
{
    public string? RowId { get; init; }
    public string? PartitionKey { get; set; }
    public long GitHubId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? HtmlUrl { get; init; }
    public DateTime? CreatedAt { get; init; }
    public string? Language { get; init; }
}