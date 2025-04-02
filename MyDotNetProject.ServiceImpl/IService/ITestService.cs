using MyDotNetProject.Common.Abstracts;
using MyDotNetProject.Entities.Dto;
using MyDotNetProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.ServiceImpl.IService
{
    public interface ITestService : IInjection
    {
        Task<List<TestDto>> GetTests();

        Task<List<TestDto>> GetAllTestCache();
    }
}
