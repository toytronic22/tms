using System.Reflection;
using log4net;
using log4net.Config;

namespace FinalSurge.Tests;

[SetUpFixture]
public class LoggingSetup
{
    [OneTimeSetUp]
    public void ConfigureLogging()
    {
        string configPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "log4net.config");
        XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetExecutingAssembly()), new FileInfo(configPath));
    }
}
