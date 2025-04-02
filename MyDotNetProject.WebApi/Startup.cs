using MyDotNetProject.Common;
using MyDotNetProject.Common.Authentication;
using MyDotNetProject.Common.MemoryCache;
using MyDotNetProject.Repository;
using MyDotNetProject.ServiceImpl;
using MyDotNetProject.WebApi.Filter;
using System;

namespace MyDotNetProject.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // 服务注册代码

            services.AddControllers(options =>
            {
                // 注册全局输出过滤器
                options.Filters.Add(typeof(GlobalResponseFilter));
                options.Filters.Add(typeof(GlobalExceptionFilter));
                options.Filters.Add(typeof(RequestLoggingFilter));
            });

            // 配置Swagger
            services.AddSwaggerGen(c =>
            {
                // 添加对文件上传的支持
                c.OperationFilter<SwaggerFileUploadFilter>();
            });
            services.AddEndpointsApiExplorer();// API探索者

            services.AddMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyOrigin()      // 允许任何来源
                        .AllowAnyMethod()      // 允许任何HTTP方法
                        .AllowAnyHeader();     // 允许任何头部信息
                });
            });

            // 业务服务注册            
            services.InitService();
            services.InitRepository(Configuration);
            services.InitCore();

            // 加载配置
            services.Configure<BasicDataCache>(Configuration.GetSection("BasicDataCache"));
            services.Configure<JwtSecurityConfig>(Configuration.GetSection("JwtSecurityConfig"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 请求管道配置代码

            // 在所有中间件之前启用CORS
            app.UseCors("AllowAll");

            //if (env.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            // 重定向HTTP请求到HTTPS
            app.UseHttpsRedirection();

            // 启用路由
            app.UseRouting();

            // 使用授权中间件
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // 映射控制器端点
            });
        }
    }
}
