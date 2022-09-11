using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Core.Security.Dtos;

namespace Application.Features.Auth.Commands.AuthRegister
{
    public class AuthRegisterCommand : IRequest<AuthRegisterDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        //public string ipAddress { get; set; }

        public class AuthRegisterCommandHandler : IRequestHandler<AuthRegisterCommand, AuthRegisterDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;

            public AuthRegisterCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<AuthRegisterDto> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.UserEmailCanNotBeDuplicatedWhenInserted(request.UserForRegisterDto.Email);

                Byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                User user = new User
                {
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };
                
                User registeredUser = await _userRepository.AddAsync(user);

                UserOperationClaim userOperationClaim = new() { UserId = registeredUser.Id, OperationClaimId = 1 };
                await _userOperationClaimRepository.AddAsync(userOperationClaim);
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id, include: u => u.Include(u => u.OperationClaim));
                IList<OperationClaim> operationClaims = userOperationClaims.Items.Select(u => new OperationClaim { Id = u.OperationClaim.Id, Name = u.OperationClaim.Name }).ToList();

                AccessToken accessToken = _tokenHelper.CreateToken(registeredUser, userOperationClaims.Items.Select(u => u.OperationClaim).ToList());
                //RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(registeredUser, request.ipAddress);
                AuthRegisterDto authRegisterDto = new() { AccessToken = accessToken, };//RefreshToken = refreshToken };

                return authRegisterDto;
            }
        }
    }
}
