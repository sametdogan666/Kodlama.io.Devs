using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.GitHubProfiles.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<GitHubProfile, CreatedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, CreateGitHubProfileCommand>().ReverseMap();

            CreateMap<GitHubProfile, DeletedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, DeleteGitHubProfileCommand>().ReverseMap();

            CreateMap<GitHubProfile, UpdatedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, UpdateGitHubProfileCommand>().ReverseMap();
        }
    }
}
