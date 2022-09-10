using Application.Features.Users.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetListClaimByUserId
{
    public class GetListClaimByUserIdQuery : IRequest<OperationClaimListModel>
    {
        public int UserId { get; set; }

        public class GetListClaimByUserIdHandler : IRequestHandler<GetListClaimByUserIdQuery, OperationClaimListModel>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public GetListClaimByUserIdHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<OperationClaimListModel> Handle(GetListClaimByUserIdQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(include: m => m.Include(c => c.User).Include(c => c.OperationClaim));

                OperationClaimListModel mappedOperationClaims =
                    _mapper.Map<OperationClaimListModel>(userOperationClaims);

                return mappedOperationClaims;
            }
        }
    }
}
