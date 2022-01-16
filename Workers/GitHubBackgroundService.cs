using ABU.GitHubApiClient.Abstractions;
using ABU.GitHubApiClient.Models;
using ABU.Portfolio.Models;
using Ardalis.GuardClauses;
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
                _logger.Log(LogLevel.Information, message: "{FullName} started at: {DateTime}", fullName, DateTime.Now);

                var scope = _provider.CreateScope();
                var client = scope.ServiceProvider.GetRequiredService<IGitHubApiClient>();

                var result = await client.GetRepositoriesForAuthUserAsync(new GitHubRepoRouteParams { PerPage = "100" }, stoppingToken);
                if (result.IsSuccessful)
                {
                    var repos = JsonConvert.DeserializeObject<IEnumerable<GitHubRepositoryModel>>(result.Json!);
                    // TODO: take repos and persist to azure storage
                }
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

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }

        _logger.Log(LogLevel.Information, "{FullName} stopped at: {DateTime}", fullName, DateTime.Now);
    }
}