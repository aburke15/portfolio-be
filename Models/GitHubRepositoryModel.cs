namespace ABU.Portfolio.Models;

public record GitHubRepositoryModel(string Name, string? Description, string HtmlUrl, DateTime CreatedAt, string? Language);