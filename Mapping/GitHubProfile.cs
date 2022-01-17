using ABU.Portfolio.Models;
using AutoMapper;
using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Mapping;

public class GitHubProfile : Profile
{
    public GitHubProfile()
    {
        CreateMap<ITableEntity, GitHubRepositoryModel>();
        CreateMap<GitHubRepositoryModel, ITableEntity>();
        CreateMap<GitHubRepositoryEntity, GitHubRepositoryModel>();
        CreateMap<GitHubRepositoryModel, GitHubRepositoryEntity>();
    }
}