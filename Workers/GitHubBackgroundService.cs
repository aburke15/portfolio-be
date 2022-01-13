using ABU.GitHubApiClient.Abstractions;
using Ardalis.GuardClauses;

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
        const string workerName = nameof(GitHubBackgroundService);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.Log(LogLevel.Information, message: $"{workerName} started at: {DateTime.Now}");
                var scope = _provider.CreateScope();

                var client = scope.ServiceProvider.GetRequiredService<IGitHubApiClient>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await Task.Delay(TimeSpan.FromDays(2), stoppingToken);
        }
        
        _logger.Log(LogLevel.Information, $"{workerName} stopped at: {DateTime.Now}");
    }
}