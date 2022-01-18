using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Models;

public class GitHubRepositoryEntity : TableEntity
{
    public GitHubRepositoryEntity()
    {
        RowId = RowKey = Guid.NewGuid()
            .ToString();
    }

    public string RowId { get; set; }
    public long GitHubId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? HtmlUrl { get; set; }
    public string? Language { get; set; }
}