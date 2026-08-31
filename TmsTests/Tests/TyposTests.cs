using OpenQA.Selenium;

namespace TmsTests.Tests;

[TestFixture]
public class TyposTests : BaseTest
{
    private const string ExpectedText = "Sometimes you'll see a typo, other times you won't.";
    private const int MaxAttempts = 10;

    [Test]
    public void Paragraph_EventuallyMatchesCorrectSpelling()
    {
        OpenPage("/typos");

        var actualText = string.Empty;
        for (var attempt = 1; attempt <= MaxAttempts; attempt++)
        {
            actualText = Driver.FindElements(By.TagName("p"))[1].Text.Trim();

            if (actualText == ExpectedText)
            {
                break;
            }

            Driver.Navigate().Refresh();
        }

        Assert.That(actualText, Is.EqualTo(ExpectedText));
    }
}
