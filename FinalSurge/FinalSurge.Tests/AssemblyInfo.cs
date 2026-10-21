using log4net.Config;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(3)]
