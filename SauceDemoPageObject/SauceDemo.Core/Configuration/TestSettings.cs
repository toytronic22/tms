namespace SauceDemo.Core.Configuration;

public class TestSettings
{
    public BrowserType Browser { get; set; }
    public bool Headless { get; set; }
    public string BaseUrl { get; set; } = string.Empty;
    public int ImplicitWaitSeconds { get; set; }
}
