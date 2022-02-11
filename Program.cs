using ABU.GitHubApiClient;
using ABU.Portfolio;
using ABU.Portfolio.Mapping;
using ABU.Portfolio.Services.Abstractions;
using ABU.Portfolio.Services.Implementations;
using ABU.Portfolio.Workers;
using Ardalis.GuardClauses;

const string portfolioOrigins = "ReactPortfolioOrigin";

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Configuration.AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
    DotEnv.LoadEnvironmentVariables();

// Add services to the container.
services.AddCors(options =>
{
    options.AddPolicy(name: portfolioOrigins,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins(
                "https://www.aburke.tech",
                "https://aburke.tech",
                "https://aburke.vercel.app",
                "http://localhost:3000"
            );
        });
});

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

const string gitHubUser = "GITHUB_USER";
const string gitHubPat = "GITHUB_PAT";

var user = Environment.GetEnvironmentVariable(gitHubUser);
var token = Environment.GetEnvironmentVariable(gitHubPat);

// User add
services.AddGitHubApiClient(options =>
{
    options.AddUsername(Guard.Against.NullOrWhiteSpace(user, nameof(user)));
    options.AddToken(Guard.Against.NullOrWhiteSpace(token, nameof(token)));
});

services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(GitHubProfile)); });
services.AddScoped<ITableStorageClient, TableStorageClient>();
services.AddTransient<ITableStorageService, TableStorageService>();
services.AddTransient<IGitHubTableStorageService, GitHubTableStorageService>();

services.AddHostedService<GitHubBackgroundService>();

var app = builder.Build();

app.UseCors(portfolioOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
