using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TmsTests.Tests;

[TestFixture]
public class NotificationMessageTests : BaseTest
{
    private static readonly By ClickHereLink = By.LinkText("Click here");
    private static readonly By Flash = By.Id("flash");

    private static readonly string[] ExpectedMessages =
    [
        "Action successful",
        "Action unsuccesful, please try again"
    ];

    [Test]
    public void ClickingLink_ShowsExpectedNotificationMessage()
    {
        OpenPage("/notification_message_rendered");
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        Driver.FindElement(ClickHereLink).Click();
        var flash = wait.Until(driver => driver.FindElement(Flash));
        var message = flash.Text.Replace("×", string.Empty).Trim();

        Assert.That(ExpectedMessages, Does.Contain(message));
    }
}
