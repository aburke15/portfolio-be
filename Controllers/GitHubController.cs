using System.Diagnostics.CodeAnalysis;
using ABU.GitHubApiClient.Abstractions;
using ABU.Portfolio.Models;
using ABU.Portfolio.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubController : ControllerBase
{
    private readonly IGitHubApiClient _client;
    private readonly ILogger<GitHubController> _logger;
    private readonly ITableStorageService _storageService;
    private readonly IMapper _mapper;

    public GitHubController(IGitHubApiClient client, ILogger<GitHubController> logger, ITableStorageService storageService, IMapper mapper)
    {
        _client = client;
        _logger = logger;
        _storageService = storageService;
        _mapper = mapper;
    }

    [HttpGet("repos/{tableName}")]
    public async Task<IActionResult> GetReposAsync(string tableName, CancellationToken ct)
    {
        // var entities = await _storageService.RetrieveAllAsync(tableName, ct);
        // var results = _mapper.Map<IEnumerable<GitHubRepositoryModel>>(entities);
        
        // return Ok(results);
        return Ok();
    }

    [HttpGet("repos/{id}/{partitionKey}")]
    public async Task<IActionResult> GetRepoByIdAsync([FromRoute] string id, [FromRoute] string partitionKey, CancellationToken ct)
    {
        var entity = await _storageService.RetrieveAsync("repos", id, partitionKey, ct);
        var result = _mapper.Map<GitHubRepositoryModel>(entity);
        
        return Ok(result);
    }
}