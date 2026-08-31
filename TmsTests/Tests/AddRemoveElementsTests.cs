using OpenQA.Selenium;

namespace TmsTests.Tests;

[TestFixture]
public class AddRemoveElementsTests : BaseTest
{
    private static readonly By AddButton = By.XPath("//button[text()='Add Element']");
    private static readonly By DeleteButtons = By.XPath("//button[text()='Delete']");

    [Test]
    public void AddTwoElements_ThenDeleteOne_ShouldLeaveOneElement()
    {
        OpenPage("/add_remove_elements/");

        Driver.FindElement(AddButton).Click();
        Driver.FindElement(AddButton).Click();
        var afterAdding = Driver.FindElements(DeleteButtons).Count;

        Driver.FindElements(DeleteButtons)[0].Click();
        var afterDeleting = Driver.FindElements(DeleteButtons).Count;

        Assert.Multiple(() =>
        {
            Assert.That(afterAdding, Is.EqualTo(2));
            Assert.That(afterDeleting, Is.EqualTo(1));
        });
    }
}
