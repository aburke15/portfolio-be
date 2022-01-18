using ABU.Portfolio.Models;
using AutoMapper;
using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Mapping;

public class GitHubProfile : Profile
{
    public GitHubProfile()
    {
        CreateMap<GitHubRepositoryModel, ITableEntity>();
        CreateMap<GitHubRepositoryModel, GitHubRepositoryEntity>();
        CreateMap<GitHubRepositoryEntity, GitHubRepositoryViewModel>();
        CreateMap<ITableEntity, GitHubRepositoryViewModel>();
    }
}