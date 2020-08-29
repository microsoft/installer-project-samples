using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InstPrj_NetCoreWinService
{
    public class Program
    {
        private static string ServiceName = "InstPrj_TestService";
        private static string ServiceExePath = Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.Length - 4) + ".exe";

        public static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0].Equals("/Install", StringComparison.OrdinalIgnoreCase))
                {
                    RunSc(string.Format("create {0} binpath=\"{1}\"", ServiceName, ServiceExePath));
                    RunSc(string.Format("start {0}", ServiceName));
                    RunSc(string.Format("config {0} start=auto", ServiceName));
                }
                else if (args[0].Equals("/Uninstall", StringComparison.OrdinalIgnoreCase))
                {
                    RunSc("stop " + ServiceName);
                    RunSc("delete " + ServiceName);
                }
                else
                {
                    Console.WriteLine("Options:");
                    Console.WriteLine($"\t/Install\tInstall and start service {ServiceName}");
                    Console.WriteLine($"\t/Uninstall\tStop and delete service {ServiceName}");
                }
            }
            else
            {
                CreateHostBuilder().Build().Run();
            }
        }

        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder(null)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                }).UseWindowsService()
                  .ConfigureLogging((_, logging) => logging.AddEventLog());

        private static void RunSc(string args)
        {
            var psi = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "sc.exe",
                Arguments = args,
            };

            var process = Process.Start(psi);
            process.WaitForExit();
        }
    }
}
