using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.Auth.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserEmailCanNotBeDuplicatedWhenInserted(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException("Email already exists.");
        }

        public async Task UserShouldBeExists(User? user)
        {
            if (user == null) throw new BusinessException("User not exists.");
        }

        public async Task UserPasswordShouldBeMatch(int id, string password)
        {
            User? user = await _userRepository.GetAsync(u => u.Id == id);
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException("Password don't match.");

        }
    }
}
