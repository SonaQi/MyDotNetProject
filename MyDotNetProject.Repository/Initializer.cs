using Microsoft.Extensions.DependencyInjection;
using MyDotNetProject.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MyDotNetProject.Repository
{
    public static class Initializer
    {
        public static void InitRepository(this IServiceCollection services, IConfiguration config)
        {
            services.Injection("MyDotNetProject.Repository");

            // 注册数据库服务
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")))
                );
        }
    }
}
