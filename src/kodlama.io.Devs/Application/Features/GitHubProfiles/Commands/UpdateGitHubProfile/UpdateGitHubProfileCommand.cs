using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommand:IRequest<UpdatedGitHubProfileDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GitHubUrl { get; set; }

        public class
            UpdateGitHubProfileCommandHandler : IRequestHandler<UpdateGitHubProfileCommand, UpdatedGitHubProfileDto>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IMapper _mapper;
            private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

            public UpdateGitHubProfileCommandHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper, GitHubProfileBusinessRules gitHubProfileBusinessRules)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _mapper = mapper;
                _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
            }

            public async Task<UpdatedGitHubProfileDto> Handle(UpdateGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                GitHubProfile? gitHubProfile = await _gitHubProfileRepository.GetAsync(g => g.Id == request.Id);

                _gitHubProfileBusinessRules.GitHubProfileShouldExistWhenRequested(gitHubProfile);
                await _gitHubProfileBusinessRules.GitHubProfileUrlCanNotBeDuplicatedWhenInsertedOrUpdated(request.GitHubUrl);

                _mapper.Map(request, gitHubProfile);
                GitHubProfile updatedGitHubProfile = await _gitHubProfileRepository.UpdateAsync(gitHubProfile);
                UpdatedGitHubProfileDto updatedGitHubProfileDto =
                    _mapper.Map<UpdatedGitHubProfileDto>(updatedGitHubProfile);

                return updatedGitHubProfileDto;

            }
        }
    }
}
