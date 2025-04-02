using MediatR;
using Microsoft.Extensions.Logging;
using MyDotNetProject.Common.Abstracts;
using MyDotNetProject.Entities.Commands;
using MyDotNetProject.Entities.Entity;
using MyDotNetProject.Repository.IRepository;

namespace MyDotNetProject.ServiceImpl.Handlers
{
    public class AddTestCommandHandler : IRequestHandler<AddTestCommand, bool>
    {
        private readonly ILogger<AddTestCommandHandler> _logger;
        private readonly ITestRepository _testRepository;
        private readonly IModelMapper _modelMapper;

        public AddTestCommandHandler(
            ILogger<AddTestCommandHandler> logger,
            ITestRepository testRepository,
            IModelMapper modelMapper)
        {
            _logger = logger;
            _testRepository = testRepository;
            _modelMapper = modelMapper;
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(AddTestCommand request, CancellationToken cancellationToken) 
        {
            var model = this._modelMapper.MapTo<Test, AddTestCommand>(request);

            return _testRepository.Insert(model);
        }
    }
}
