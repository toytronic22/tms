using HerokuApp.com.demoqa.Pages;
using OpenQA.Selenium.Chrome;

namespace HerokuApp.com.demoqa.Tests;

public class FileDownloadTests : BaseTest
{
    private const string DownloadedFileName = "sampleFile.jpeg";

    private string _downloadFolder = string.Empty;

    protected override ChromeOptions GetChromeOptions()
    {
        _downloadFolder = Path.Combine(Path.GetTempPath(), "demoqa_downloads_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_downloadFolder);

        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddUserProfilePreference("download.default_directory", _downloadFolder);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("download.directory_upgrade", true);
        return options;
    }

    [Test]
    public void DownloadedFileIsSavedToDisk()
    {
        UploadDownloadPage page = new UploadDownloadPage(driver).Open();
        page.ClickDownload();

        string filePath = Path.Combine(_downloadFolder, DownloadedFileName);
        Assert.That(WaitForFile(filePath), Is.True, $"Файл не появился на диске: {filePath}");

        FileInfo file = new FileInfo(filePath);
        Assert.Multiple(() =>
        {
            Assert.That(file.Name, Is.EqualTo(page.GetDownloadedFileName()));
            Assert.That(file.Length, Is.GreaterThan(0));
        });
    }

    [TearDown]
    public void DeleteDownloads()
    {
        if (Directory.Exists(_downloadFolder))
        {
            Directory.Delete(_downloadFolder, true);
        }
    }

    private static bool WaitForFile(string filePath, int timeoutSeconds = 20)
    {
        DateTime deadline = DateTime.Now.AddSeconds(timeoutSeconds);
        while (DateTime.Now < deadline)
        {
            if (File.Exists(filePath))
            {
                return true;
            }

            Thread.Sleep(200);
        }

        return false;
    }
}
