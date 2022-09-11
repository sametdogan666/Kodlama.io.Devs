using Core.Security.JWT;

namespace Application.Features.Auth.Dtos
{
    public class AuthLoginDto
    {
        public AccessToken? AccessToken { get; set; }
        //public RefreshToken RefreshToken { get; set; }
        //public AuthenticatorType? AuthenticatorType { get; set; }

        public class LoginResponseDto
        {
            public AccessToken? AccessToken { get; set; }
            //public AuthenticatorType? AuthenticatorType { get; set; }
        }

        public LoginResponseDto CreateResponseDto()
        {
            return new() { AccessToken = AccessToken }; //AuthenticatorType = AuthenticatorType };
        }

       
    }
}
