using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AthsEssGymBook.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Diagnostics.Debug.WriteLine("ss=======================================");
            System.Diagnostics.Debug.WriteLine(args.Length);
            System.Diagnostics.Debug.WriteLine("ss=======================================");
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .UseStartup<Startup>()
                .Build();
    }
}
