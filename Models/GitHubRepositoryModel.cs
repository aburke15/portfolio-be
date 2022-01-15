using Newtonsoft.Json;

namespace ABU.Portfolio.Models;

public record GitHubRepositoryModel
{
    [JsonProperty("name")]
    public string? Name { get; init; }
    [JsonProperty("description")]
    public string? Description { get; init; }
    [JsonProperty("html_url")]
    public string? HtmlUrl { get; init; }
    [JsonProperty("created_at")]
    public DateTime? CreatedAt { get; init; }
    [JsonProperty("language")]
    public string? Language { get; init; }
}