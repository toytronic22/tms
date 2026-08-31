using HerokuApp.com.demoqa.Pages;

namespace HerokuApp.com.demoqa.Tests;

public class FileUploadTests : BaseTest
{
    [Test]
    public void UploadedFileNameIsShownOnPage()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "TestData", "UploadFile.txt");
        Assert.That(File.Exists(filePath), Is.True, $"Файл для загрузки не найден: {filePath}");

        UploadDownloadPage page = new UploadDownloadPage(driver).Open();
        page.UploadFile(filePath).WaitForUploadedFilePath();

        Assert.That(page.GetUploadedFileName(), Is.EqualTo(Path.GetFileName(filePath)));
    }
}
