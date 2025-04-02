using MyDotNetProject.Entities.Entity;
using MyDotNetProject.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Repository.Repository
{
    public class TestRepository : BaseRepository<Test>, ITestRepository
    {
        private readonly AppDbContext _mydbcontext;

        public TestRepository(AppDbContext mydbcontext) : base(mydbcontext)
        {
            _mydbcontext = mydbcontext;
        }

        public async Task<List<Test>> GetAll()
        {
            return _mydbcontext.Tests.ToList();
        }
    }
}
