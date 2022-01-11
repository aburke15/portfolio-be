using ABU.GitHubApiClient.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ABU.Portfolio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubController : ControllerBase
{
    private readonly IGitHubApiClient _client;
    
    public GitHubController(IGitHubApiClient client)
    {
        _client = client;
    }

    [HttpGet("repos")]
    public async Task<IActionResult> GetReposAsync()
    {
        var result = await _client.GetRepositoriesForUserAsync();
        return Ok(result.Json);
    }
}