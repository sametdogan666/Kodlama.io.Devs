using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.GitHubProfiles.Rules
{
    public class GitHubProfileBusinessRules
    {
        private readonly IGitHubProfileRepository _gitHubProfileRepository;

        public GitHubProfileBusinessRules(IGitHubProfileRepository gitHubProfileRepository)
        {
            _gitHubProfileRepository = gitHubProfileRepository;
        }

        public async Task GitHubProfileUrlCanNotBeDuplicatedWhenInsertedOrUpdated(string gitHubUrl)
        {
            IPaginate<GitHubProfile> result =
                await _gitHubProfileRepository.GetListAsync(p => p.GitHubUrl == gitHubUrl);
            if (result.Items.Any()) throw new BusinessException("GitHub profile already exists");
        }

        public void GitHubProfileShouldExistWhenRequested(GitHubProfile gitHubProfile)
        {
            if (gitHubProfile == null) throw new BusinessException("Requested gitHub profile does not exists");
        }
    }
}
