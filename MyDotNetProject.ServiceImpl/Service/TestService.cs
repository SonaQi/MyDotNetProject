using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyDotNetProject.Common.Abstracts;
using MyDotNetProject.Common.Extensions;
using MyDotNetProject.Common.MemoryCache;
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
        private readonly IMemoryCacheService _cacheService;
        private readonly int cacheSeconds;
        private static object obj = new object();


        public TestService(IOptions<BasicDataCache> options, ITestRepository testRepository,
            ILogger<TestService> logger,
            IModelMapper modelMapper, 
            IMemoryCacheService cacheService)
        {
            _testRepository = testRepository;
            _logger = logger;
            _modelMapper = modelMapper;
            _cacheService = cacheService;

            cacheSeconds = options.Value.CacheSeconds;
            _cacheService = cacheService;
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

        public async Task<List<TestDto>> GetAllTestCache() 
        {
            string key = "key_test";
            List<TestDto> tests = this._cacheService.GetList<TestDto>(key);
            if (tests != null && tests.Count > 0)
            {
                return tests;
            }
            lock (obj)
            {
                tests = this.GetTests().Result;
                if (tests != null && tests.Count > 0)
                {
                    this._cacheService.Set(key, tests, new TimeSpan(0, 0, cacheSeconds));
                }
                return tests;
            }
        }
    }
}
