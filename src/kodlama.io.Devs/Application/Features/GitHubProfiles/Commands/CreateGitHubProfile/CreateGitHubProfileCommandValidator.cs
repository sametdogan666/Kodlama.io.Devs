using FluentValidation;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile
{
    public class CreateGitHubProfileCommandValidator:AbstractValidator<CreateGitHubProfileCommand>
    {
        public CreateGitHubProfileCommandValidator()
        {
            RuleFor(g => g.GitHubUrl).NotEmpty();
            RuleFor(g=>g.UserId).NotEmpty();
        }
    }
}
