using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.AuthLogin
{
    public class AuthLoginCommand:IRequest<AuthLoginDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string ipAddress { get; set; }

        public class AuthLoginCommandHandler : IRequestHandler<AuthLoginCommand, AuthLoginDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;

            public AuthLoginCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
                _tokenHelper = tokenHelper;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<AuthLoginDto> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(x => x.Email == request.UserForLoginDto.Email);
                
                await _authBusinessRules.UserShouldBeExists(user);
                await _authBusinessRules.UserPasswordShouldBeMatch(user.Id, request.UserForLoginDto.Password);

                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id, include: u => u.Include(u => u.OperationClaim));

                AccessToken accessToken = _tokenHelper.CreateToken(user, userOperationClaims.Items.Select(u => u.OperationClaim).ToList());
                RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, request.ipAddress);
                //RefreshToken addedRefreshToken = await _authService.AddRefreshToken(refreshToken);
               
                AuthLoginDto authLoginDto = new()
                {
                    AccessToken = accessToken,
                    //RefreshToken = refreshToken
                };
                return authLoginDto;
            }
        }
    }
}
