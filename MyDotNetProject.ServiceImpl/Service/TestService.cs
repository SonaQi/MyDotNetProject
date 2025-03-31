using Microsoft.Extensions.Logging;
using MyDotNetProject.Common.Abstracts;
using MyDotNetProject.Common.Extensions;
using MyDotNetProject.Entities.Dto;
using MyDotNetProject.Entities.Entity;
using MyDotNetProject.Repository;
using MyDotNetProject.ServiceImpl.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.ServiceImpl.Service
{
    public class TestService : ITestService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<TestService> _logger;
        private readonly IModelMapper _modelMapper;

        public TestService(AppDbContext appDbContext,
            ILogger<TestService> logger,
            IModelMapper modelMapper) {
            _appDbContext = appDbContext;
            _logger = logger;
            _modelMapper = modelMapper;
        }

        public async Task<List<TestDto>> GetTests()
        {
            var list = _appDbContext.Tests.ToList();
            var testDtos = new List<TestDto>();
            foreach (var item in list)
            {
                var model = this._modelMapper.MapTo<TestDto, Test>(item);
                testDtos.Add(model);
            }
            
            _logger.LogInformation(list.ToJson());
            return testDtos;
        }
    }
}
