using ABU.GitHubApiClient.Abstractions;
using Ardalis.GuardClauses;

namespace ABU.Portfolio.Workers;

public class GitHubBackgroundService : BackgroundService
{
    private readonly ILogger<GitHubBackgroundService> _logger;
    private readonly IGitHubApiClient _client; 

    public GitHubBackgroundService(ILogger<GitHubBackgroundService> logger, IGitHubApiClient client)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _client = Guard.Against.Null(client, nameof(client));
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // TODO: log when it starts

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await Task.Delay(TimeSpan.FromDays(2), stoppingToken);
        }
        
        // TODO: log when it stops
    }
}