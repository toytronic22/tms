using OpenQA.Selenium;

namespace TmsTests.Tests;

[TestFixture]
public class InputsTests : BaseTest
{
    private static readonly By Input = By.TagName("input");

    [Test]
    public void ArrowKeys_ChangeNumberValue_UpAndDown()
    {
        OpenPage("/inputs");
        var input = Driver.FindElement(Input);

        input.SendKeys(Keys.ArrowUp);
        var afterFirstUp = input.GetAttribute("value");

        input.SendKeys(Keys.ArrowUp);
        var afterSecondUp = input.GetAttribute("value");

        input.SendKeys(Keys.ArrowDown);
        var afterDown = input.GetAttribute("value");

        Assert.Multiple(() =>
        {
            Assert.That(afterFirstUp, Is.EqualTo("1"));
            Assert.That(afterSecondUp, Is.EqualTo("2"));
            Assert.That(afterDown, Is.EqualTo("1"));
        });
    }

    [Test]
    public void NumericValue_IsAccepted_AndTextValue_IsIgnored()
    {
        OpenPage("/inputs");
        var input = Driver.FindElement(Input);

        input.SendKeys("2026");
        var numericValue = input.GetAttribute("value");

        input.Clear();
        input.SendKeys("abc");
        var textValue = input.GetAttribute("value");

        Assert.Multiple(() =>
        {
            Assert.That(numericValue, Is.EqualTo("2026"));
            Assert.That(textValue, Is.Empty);
        });
    }
}
