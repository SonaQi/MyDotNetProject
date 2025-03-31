using Microsoft.Extensions.DependencyInjection;
using MyDotNetProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.ServiceImpl
{
    public static class Initializer
    {
        public static void InitService(this IServiceCollection services)
        {
            services.Injection("MyDotNetProject.ServiceImpl");
        }
    }
}
