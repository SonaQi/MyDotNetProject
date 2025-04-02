using Microsoft.Extensions.Logging;
using MyDotNetProject.Common.Abstracts;
using MyDotNetProject.Common.Extensions;
using MyDotNetProject.Entities.Dto;
using MyDotNetProject.Entities.Entity;
using MyDotNetProject.Repository.IRepository;
using MyDotNetProject.ServiceImpl.IService;

namespace MyDotNetProject.ServiceImpl.Service
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly ILogger<TestService> _logger;
        private readonly IModelMapper _modelMapper;

        public TestService(ITestRepository testRepository,
            ILogger<TestService> logger,
            IModelMapper modelMapper) {
            _testRepository = testRepository;
            _logger = logger;
            _modelMapper = modelMapper;
        }

        public async Task<List<TestDto>> GetTests()
        {
            var list = await _testRepository.GetAll();
            //_testRepository.GetEntities(x => x.id == 1).ToList();
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
