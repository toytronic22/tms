using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TmsTests.Tests;

[TestFixture]
public class DropdownTests : BaseTest
{
    private static readonly By Dropdown = By.Id("dropdown");

    [Test]
    public void AllOptions_ArePresent_AndCanBeSelectedOneByOne()
    {
        OpenPage("/dropdown");
        var select = new SelectElement(Driver.FindElement(Dropdown));

        var optionTexts = select.Options.Select(option => option.Text).ToList();

        select.SelectByText("Option 1");
        var firstSelected = select.SelectedOption.Text;

        select.SelectByText("Option 2");
        var secondSelected = select.SelectedOption.Text;

        Assert.Multiple(() =>
        {
            Assert.That(optionTexts, Is.EqualTo(new[] { "Please select an option", "Option 1", "Option 2" }));
            Assert.That(firstSelected, Is.EqualTo("Option 1"));
            Assert.That(secondSelected, Is.EqualTo("Option 2"));
        });
    }
}
