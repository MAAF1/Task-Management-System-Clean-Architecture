using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<RegisterRequest, ApplicationUser>();
            CreateMap<CreateTaskDto, TaskEntity>();



        }
    }
}
