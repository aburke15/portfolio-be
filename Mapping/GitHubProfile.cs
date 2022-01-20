using ABU.Portfolio.Models;
using AutoMapper;
using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Mapping;

public class GitHubProfile : Profile
{
    public GitHubProfile()
    {
        CreateMap<GitHubRepositoryApiResponse, ITableEntity>();
        CreateMap<GitHubRepositoryApiResponse, GitHubRepositoryEntity>();
        CreateMap<GitHubRepositoryEntity, GitHubRepositoryResponse>();
        CreateMap<ITableEntity, GitHubRepositoryResponse>();
    }
}