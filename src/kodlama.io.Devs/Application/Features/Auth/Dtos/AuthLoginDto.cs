using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
