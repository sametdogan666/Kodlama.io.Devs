using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.Users.Profiles
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IPaginate<UserOperationClaim>, OperationClaimListModel>().ForMember(c => c.Items, opt => opt.MapFrom(c => c.Items)).ReverseMap();
            CreateMap<UserOperationClaim, OperationClaimListDto>().ForMember(C => C.Name, opt => opt.MapFrom(c => c.OperationClaim.Name)).ReverseMap();
        }
    }
}
