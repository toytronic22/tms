using OpenQA.Selenium;

namespace TmsTests.Tests;

[TestFixture]
public class CheckboxesTests : BaseTest
{
    private static readonly By Checkboxes = By.CssSelector("[type=checkbox]");

    [Test]
    public void FirstCheckbox_IsUnchecked_AndCanBeChecked()
    {
        OpenPage("/checkboxes");
        var firstCheckbox = Driver.FindElements(Checkboxes)[0];

        var wasSelectedBefore = firstCheckbox.Selected;
        firstCheckbox.Click();
        var isSelectedAfter = firstCheckbox.Selected;

        Assert.Multiple(() =>
        {
            Assert.That(wasSelectedBefore, Is.False);
            Assert.That(isSelectedAfter, Is.True);
        });
    }

    [Test]
    public void SecondCheckbox_IsChecked_AndCanBeUnchecked()
    {
        OpenPage("/checkboxes");
        var secondCheckbox = Driver.FindElements(Checkboxes)[1];

        var wasSelectedBefore = secondCheckbox.Selected;
        secondCheckbox.Click();
        var isSelectedAfter = secondCheckbox.Selected;

        Assert.Multiple(() =>
        {
            Assert.That(wasSelectedBefore, Is.True);
            Assert.That(isSelectedAfter, Is.False);
        });
    }
}
