using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Models;

public class GitHubRepositoryEntity : TableEntity
{
    public string Id { get; set; } = new Guid().ToString();
    public long GitHubId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? HtmlUrl { get; set; }
    public string? Language { get; set; }
}