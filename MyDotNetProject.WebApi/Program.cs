using Serilog;
using Serilog.Events;

namespace MyDotNetProject.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "MyDotNetProject.WebApi.log"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://*:8080");
                webBuilder.UseStartup<Startup>();
            }).UseSerilog();
        //.UseWindowsService()
        //.ConfigureServices(services =>
        //{
        //    services.AddHostedService<Worker>();
        //})

    }

}