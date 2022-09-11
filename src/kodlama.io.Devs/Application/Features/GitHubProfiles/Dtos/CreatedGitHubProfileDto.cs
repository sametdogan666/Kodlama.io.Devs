namespace Application.Features.GitHubProfiles.Dtos
{
    public class CreatedGitHubProfileDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GitHubUrl { get; set; }
    }
}
