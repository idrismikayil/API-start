using AutoMapper;
using Domain.Dtos;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(s => s.FirstName, s => s.MapFrom(src => src.Name))
                .ForMember(s => s.LastName, s => s.MapFrom(src => src.Surname))
                .ReverseMap();

            CreateMap<Student, StudentPostDto>().ReverseMap();
        }
    }
}
