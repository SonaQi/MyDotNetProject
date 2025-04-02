using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDotNetProject.Common.Abstracts;
using MyDotNetProject.Common.Common;
using MyDotNetProject.Common.Mapper;
using MyDotNetProject.Common.MemoryCache;
using MyDotNetProject.Common.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common
{
    public static class Initializer
    {
        /// <summary>
        ///     基础设施初始化.
        /// </summary>
        /// <param name="services">容器.</param>
        public static void InitCore(this IServiceCollection services) 
        {
            services.AddSingleton<HttpClientProvider>();
            services.AddSingleton<ExcelHelper>();
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();

            services.AddSingleton<IModelMapper>(sp =>
            {
                using (var scope = sp.CreateScope())
                {
                    var profiles = scope.ServiceProvider.GetServices<IProfile>();

                    var mapConfig = new MapperConfiguration(e =>
                    {
                        foreach (var profile in profiles)
                        {
                            if (profile is Profile p)
                            {
                                e.AddProfile(p);
                            }
                        }
                    });

                    var mapper = mapConfig.CreateMapper();

                    return new ModelMapper(mapper);
                }
            });
        }
    }
}
