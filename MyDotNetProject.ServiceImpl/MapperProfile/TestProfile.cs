using AutoMapper;
using MyDotNetProject.Common.Abstracts;
using MyDotNetProject.Common.Mapper;
using MyDotNetProject.Entities.Commands;
using MyDotNetProject.Entities.Dto;
using MyDotNetProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.ServiceImpl.MapperProfile
{
    public class TestProfile : Profile, IProfile
    {
        public TestProfile()
        {
            this.SourceMemberNamingConvention = new UpperUnderscoreNamingConvention();
            this.DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            this.CreateMap<Test, TestDto>().ReverseMap();
            this.CreateMap<Test, AddTestCommand>().ReverseMap();
        }
    }
}
