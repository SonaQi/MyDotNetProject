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
        /// ������������
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
            return File(_excelHelper.GetByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "���Ե���.xlsx");
        }

        [HttpPost]
        public async Task<BaseResponse<bool>> ImportTest([FromBody] string fileUrl) 
        {
            #region ��ȡexcel
            TestDto[] rows;
            try
            {
                HttpResponseMessage response = await new HttpClient().GetAsync(fileUrl);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("fileUrl��Ч");
                }

                _excelHelper.InitFromStream(await response.Content.ReadAsStreamAsync());
                rows = _excelHelper.GetObjectList<TestDto>(0, 0, 1).ToArray();
            }
            catch (Exception e)
            {
                throw new Exception($"�ļ����ǺϷ���Excel�ļ�,����{e.Message}");
            }

            #endregion

            // ����rows

            return new BaseResponse<bool> { data = true };
        }

        /// <summary>
        /// ��ȡ��֤��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetVerificationCode()
        {
            // ���������֤���ַ���
            string captchaText = VerifyCodeHelper.GenerateRandomCaptchaText(4);

            // ������֤��ͼƬ
            byte[] imageBytes = VerifyCodeHelper.CreateValidateGraphic(captchaText);

            // ����ͼƬ
            //return new JsonResult(new { File = File(imageBytes, "image/png"), Data = captchaText });
            return File(imageBytes, "image/png");
        }        
    }
}