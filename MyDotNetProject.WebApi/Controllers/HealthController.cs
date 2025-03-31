using Microsoft.AspNetCore.Mvc;
using MyDotNetProject.Entities;
using MyDotNetProject.Entities.Dto;
using MyDotNetProject.Entities.Entity;
using MyDotNetProject.ServiceImpl.IService;

namespace MyDotNetProject.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class HealthController : ControllerBase
    {

        private readonly ILogger<HealthController> _logger;
        private readonly ITestService _testService;

        public HealthController(ILogger<HealthController> logger,
            ITestService testService)
        {
            _logger = logger;
            _testService= testService;
        }

        [HttpGet]
        public String Test()
        {
            return "ok";
        }

        [HttpGet]
        public async Task<BaseResponse<List<TestDto>>> GetTests() {

            var result = await _testService.GetTests();
            return new BaseResponse<List<TestDto>> { data = result };
        }
    }
}