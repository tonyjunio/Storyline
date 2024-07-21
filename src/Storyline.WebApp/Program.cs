using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Reflection;

namespace Storyline.WebApp;

public class Program
{
    static string assemblyPath = "";
    static string environmentName = "";
    public static void Main(string[] args)
    {
        assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly()?.Location) ?? "";
        if (assemblyPath is "")
        {
            throw new DirectoryNotFoundException(assemblyPath);
        }

        environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.ConfigureAppConfiguration(c =>
                    c.SetBasePath(assemblyPath)
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                );
            });
}
