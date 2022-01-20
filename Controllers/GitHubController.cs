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
    private readonly ILogger<GitHubController> _logger;
    private readonly IGitHubTableStorageService _storageService;
    private readonly IMapper _mapper;

    public GitHubController(ILogger<GitHubController> logger, IMapper mapper, IGitHubTableStorageService storageService)
    {
        _logger = logger;
        _mapper = mapper;
        _storageService = storageService;
    }

    [HttpGet("repos")]
    public async Task<IActionResult> GetReposAsync(CancellationToken ct)
    {
        var entities = await _storageService.RetrieveAllAsync(ct);
        var results = entities.Select(entity =>
            _mapper.Map<GitHubRepositoryResponse>(entity)
        );

        return Ok(results);
    }

    [HttpGet("repos/{partitionKey}/{rowId}")]
    public async Task<IActionResult> GetRepoByIdAsync([FromRoute] string partitionKey, [FromRoute] string rowId, CancellationToken ct)
    {
        var entity = await _storageService.RetrieveAsync(partitionKey, rowId, ct);
        var result = _mapper.Map<GitHubRepositoryResponse>(entity);

        return Ok(result);
    }
}