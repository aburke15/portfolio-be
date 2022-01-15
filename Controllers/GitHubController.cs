using ABU.GitHubApiClient.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ABU.Portfolio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubController : ControllerBase
{
    private readonly IGitHubApiClient _client;
    private readonly ILogger<GitHubController> _logger;

    public GitHubController(IGitHubApiClient client, ILogger<GitHubController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpGet("repos")]
    public async Task<IActionResult> GetReposAsync()
    {
        // var result = await _client.GetRepositoriesForUserAsync();
        
        // map to object
        
        // return Ok(result.Json);
        return Ok();
    }
}