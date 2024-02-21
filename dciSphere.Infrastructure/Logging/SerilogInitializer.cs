using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Logging;
public class SerilogInitializer
{
    public static ILogger Initialize(LoggingOptions options)
    {
#if DEBUG
        Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
#endif
        var logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .WriteTo.Console(
                //restrictedToMinimumLevel: LogEventLevel.Warning,
                outputTemplate: "[{Timestamp:dd-MM-yyyy} - {Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Information)
            .CreateLogger();
        return logger;
    }
}
