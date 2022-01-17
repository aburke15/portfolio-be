using ABU.GitHubApiClient;
using ABU.Portfolio;
using ABU.Portfolio.Mapping;
using ABU.Portfolio.Services.Abstractions;
using ABU.Portfolio.Services.Implementations;
using ABU.Portfolio.Workers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

DotEnv.LoadEnvironmentVariables();
builder.Configuration.AddEnvironmentVariables();
// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// User add
services.AddGitHubApiClient(options =>
{
    options.AddToken(Environment.GetEnvironmentVariable("DOTNET_GITHUB_TOKEN")!);
    options.AddUsername(Environment.GetEnvironmentVariable("DOTNET_GITHUB_USER")!);
});

services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(GitHubProfile)); });
services.AddScoped<ITableStorageClient, TableStorageClient>();
services.AddTransient<ITableStorageService, TableStorageService>();

services.AddHostedService<GitHubBackgroundService>();

var app = builder.Build();

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
