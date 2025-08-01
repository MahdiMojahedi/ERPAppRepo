using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapperProfile
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Master, MasterDto>().ReverseMap();  
            CreateMap<Subsidiary, SubsidiaryDto>().ReverseMap();  

            CreateMap<List<Subsidiary>, List<SubsidiaryDto>>().ReverseMap();
            CreateMap<List<Master>, List<MasterDto>>().ReverseMap();
        }


    }
}
