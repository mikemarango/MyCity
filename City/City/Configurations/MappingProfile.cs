using AutoMapper;
using City.DTOs;
using City.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Citi, CitiDto>();
            CreateMap<Citi, CitiNoAttractionDto>();
            CreateMap<Attraction, AttractionDto>();
            CreateMap<AttractionCreateDto, Attraction>();
            CreateMap<AttractionUpdateDto, Attraction>();
            CreateMap<Attraction, AttractionUpdateDto>();
        }
    }
}
