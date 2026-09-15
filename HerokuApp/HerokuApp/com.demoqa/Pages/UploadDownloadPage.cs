using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HerokuApp.com.demoqa.Pages;

public class UploadDownloadPage
{
    private readonly IWebDriver _driver;

    private readonly By _inpUploadFile = By.Id("uploadFile");
    private readonly By _lblUploadedFilePath = By.Id("uploadedFilePath");
    private readonly By _btnDownload = By.Id("downloadButton");

    public UploadDownloadPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public UploadDownloadPage Open()
    {
        _driver.Navigate().GoToUrl("https://demoqa.com/upload-download");
        return this;
    }

    public UploadDownloadPage UploadFile(string filePath)
    {
        _driver.FindElement(_inpUploadFile).SendKeys(filePath);
        return this;
    }

    public UploadDownloadPage WaitForUploadedFilePath()
    {
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElements(_lblUploadedFilePath).Count > 0);
        return this;
    }

    public string GetUploadedFileName()
    {
        string path = _driver.FindElement(_lblUploadedFilePath).Text;
        return path.Split('\\', '/').Last();
    }

    public UploadDownloadPage ClickDownload()
    {
        _driver.FindElement(_btnDownload).Click();
        return this;
    }

    public string GetDownloadedFileName() =>
        _driver.FindElement(_btnDownload).GetAttribute("download") ?? string.Empty;
}
