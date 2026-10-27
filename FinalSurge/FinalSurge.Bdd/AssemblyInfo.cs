using log4net.Config;
using NUnit.Framework;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(3)]
