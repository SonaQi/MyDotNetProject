using MyDotNetProject.Common.Abstracts;
using MyDotNetProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Repository.IRepository
{
    public interface ITestRepository : IBaseRepository<Test>, IInjection
    {
        Task<List<Test>> GetAll();
    }
}
