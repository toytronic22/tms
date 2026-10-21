namespace FinalSurge.Core.Configuration;

public class TestSettings
{
    public BrowserType Browser { get; set; }
    public bool Headless { get; set; }
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiUrl { get; set; } = string.Empty;
    public int ImplicitWaitSeconds { get; set; }
    public string GridUrl { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
