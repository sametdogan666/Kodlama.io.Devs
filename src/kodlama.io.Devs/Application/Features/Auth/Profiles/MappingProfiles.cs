using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auth.Commands.AuthRegister;
using Application.Features.Auth.Dtos;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Dtos;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.Auth.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            //CreateMap<User, AuthRegisterDto>().ReverseMap();
            //CreateMap<User, AuthRegisterCommand>().ReverseMap();
        }
    }
}
