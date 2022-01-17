using ABU.GitHubApiClient.Abstractions;
using ABU.GitHubApiClient.Models;
using ABU.Portfolio.Models;
using ABU.Portfolio.Services.Abstractions;
using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;

namespace ABU.Portfolio.Workers;

public class GitHubBackgroundService : BackgroundService
{
    private readonly ILogger<GitHubBackgroundService> _logger;
    private readonly IServiceProvider _provider;

    public GitHubBackgroundService(ILogger<GitHubBackgroundService> logger, IServiceProvider provider)
    {
        _provider = Guard.Against.Null(provider, nameof(provider));
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var methodInfo = System.Reflection.MethodBase.GetCurrentMethod();
        var fullName = $"{methodInfo?.DeclaringType?.FullName} - {methodInfo?.Name}";

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.Log(
                    LogLevel.Information, message:
                    "{FullName} started at: {DateTime}",
                    fullName,
                    DateTime.Now
                );

                var scope = _provider.CreateScope();
                
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                var storageService = scope.ServiceProvider.GetRequiredService<ITableStorageService>();
                var client = scope.ServiceProvider.GetRequiredService<IGitHubApiClient>();
                var result = await client.GetRepositoriesForAuthUserAsync(new GitHubRepoRouteParams { PerPage = "100" }, stoppingToken);

                if (!result.IsSuccessful)
                {
                    _logger.Log(
                        LogLevel.Warning,
                        "GitHub API client didn't respond with successful result at: {DateTime}, in => {FullName}. Message: {Message}",
                        DateTime.Now,
                        fullName,
                        result.Message
                    );
                    
                    continue;
                }

                var json = Guard.Against.NullOrWhiteSpace(result.Json, nameof(result.Json));
                var repos = JsonConvert.DeserializeObject<IEnumerable<GitHubRepositoryModel>>(json);

                if (repos is null) continue;

                var entities = repos.Select(repo =>
                    mapper.Map<GitHubRepositoryModel, GitHubRepositoryEntity>(repo, opt =>
                    {
                        opt.AfterMap((src, dest) =>
                        {
                            dest.GitHubId = src.Id;
                            dest.PartitionKey = "repository";
                        });
                    })
                );

                _ = await storageService.InsertManyAsync("repos", entities, stoppingToken);

                _logger.Log(
                    LogLevel.Information,
                    "{FullName} completed processing at: {DateTime}",
                    fullName,
                    DateTime.Now
                );
            }
            catch (Exception ex)
            {
                _logger.Log(
                    LogLevel.Error,
                    ex,
                    "An unexpected error occurred at: {DateTime}, in => {FullName}. Message: {Message}",
                    DateTime.Now,
                    fullName,
                    ex.Message
                );
            }

            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }

        _logger.Log(
            LogLevel.Information,
            "{FullName} stopped at: {DateTime}",
            fullName,
            DateTime.Now
        );
    }
}