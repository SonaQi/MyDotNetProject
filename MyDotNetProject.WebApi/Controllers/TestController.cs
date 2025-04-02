using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDotNetProject.Common.Common;
using MyDotNetProject.Entities;
using MyDotNetProject.Entities.Commands;
using MyDotNetProject.Entities.Dto;
using MyDotNetProject.ServiceImpl.IService;
using System.Net;

namespace MyDotNetProject.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;
        private readonly ITestService _testService;
        private readonly ExcelHelper _excelHelper;
        private readonly IMediator _mediator;

        public TestController(ILogger<TestController> logger,
            ITestService testService,
            ExcelHelper excelHelper,
            IMediator mediator)
        {
            _logger = logger;
            _testService = testService;
            _excelHelper = excelHelper;
            _mediator = mediator;
        }

        [HttpGet]
        public String Health()
        {
            return "ok";
        }

        /// <summary>
        /// 新增测试数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseResponse<bool>> AddTest([FromBody] AddTestCommand command)
        {
            var result = await this._mediator.Send(command);
            return new BaseResponse<bool> { data = result };
        }        

        [HttpGet]
        public async Task<BaseResponse<List<TestDto>>> GetTests()
        {

            var result = await _testService.GetTests();
            return new BaseResponse<List<TestDto>> { data = result };
        }

        [HttpGet]
        public async Task<BaseResponse<List<TestDto>>> GetAllTestCache()
        {
            var result = await _testService.GetAllTestCache();
            return new BaseResponse<List<TestDto>> { data = result };
        }

        [HttpGet]
        public async Task<ActionResult<object>> ExportTests()
        {
            var result = await _testService.GetTests();

            _excelHelper.InitFromObjectList(result);
            return File(_excelHelper.GetByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "测试导出.xlsx");
        }

        [HttpPost]
        public async Task<BaseResponse<bool>> ImportTest([FromBody] string fileUrl) 
        {
            #region 读取excel
            TestDto[] rows;
            try
            {
                HttpResponseMessage response = await new HttpClient().GetAsync(fileUrl);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("fileUrl无效");
                }

                _excelHelper.InitFromStream(await response.Content.ReadAsStreamAsync());
                rows = _excelHelper.GetObjectList<TestDto>(0, 0, 1).ToArray();
            }
            catch (Exception e)
            {
                throw new Exception($"文件不是合法的Excel文件,错误：{e.Message}");
            }

            #endregion

            // 处理rows

            return new BaseResponse<bool> { data = true };
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetVerificationCode()
        {
            // 生成随机验证码字符串
            string captchaText = VerifyCodeHelper.GenerateRandomCaptchaText(4);

            // 创建验证码图片
            byte[] imageBytes = VerifyCodeHelper.CreateValidateGraphic(captchaText);

            // 返回图片
            //return new JsonResult(new { File = File(imageBytes, "image/png"), Data = captchaText });
            return File(imageBytes, "image/png");
        }        
    }
}