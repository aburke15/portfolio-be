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
            catch (Exception ex)
            {
                var methodInfo = System.Reflection.MethodBase.GetCurrentMethod();
                var fullName = $"{methodInfo?.DeclaringType?.FullName} - {methodInfo?.Name}";
                
                _logger.Log(LogLevel.Error, $"An unexpected error occurred at: {DateTime.Now}, in => {fullName}. Message: {ex.Message}", ex);
            }

            await Task.Delay(TimeSpan.FromDays(2), stoppingToken);
        }
        
        _logger.Log(LogLevel.Information, $"{workerName} stopped at: {DateTime.Now}");
    }
}