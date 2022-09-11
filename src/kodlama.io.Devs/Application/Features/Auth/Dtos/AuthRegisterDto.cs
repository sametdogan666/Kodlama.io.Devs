using Core.Security.JWT;

namespace Application.Features.Auth.Dtos
{
    public class AuthRegisterDto
    {
        public AccessToken AccessToken { get; set; }
       // public RefreshToken RefreshToken { get; set; }
    }
}
