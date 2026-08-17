using OpenQA.Selenium;

namespace SauceDemoLocators;

[TestFixture]
public class BasicLocatorsTests : BaseTest
{
    [Test]
    public void ById_FindsLoginButton()
    {
        var element = Driver.FindElement(By.Id("login-button"));

        Assert.That(element.GetAttribute("value"), Is.EqualTo("Login"));
    }

    [Test]
    public void ByName_FindsUsernameInput()
    {
        var element = Driver.FindElement(By.Name("user-name"));

        Assert.That(element.GetAttribute("placeholder"), Is.EqualTo("Username"));
    }

    [Test]
    public void ByClassName_FindsLoginLogo()
    {
        var element = Driver.FindElement(By.ClassName("login_logo"));

        Assert.That(element.Text, Is.EqualTo("Swag Labs"));
    }

    [Test]
    public void ByTagName_FindsAllInputsOnLoginForm()
    {
        var elements = Driver.FindElements(By.TagName("input"));

        Assert.That(elements, Has.Count.EqualTo(3));
    }

    [Test]
    public void ByLinkText_FindsTwitterLinkInFooter()
    {
        LogIn();

        var element = Driver.FindElement(By.LinkText("Twitter"));

        Assert.That(element.GetAttribute("href"), Does.Contain("twitter.com"));
    }

    [Test]
    public void ByPartialLinkText_FindsProductLink()
    {
        LogIn();

        var element = Driver.FindElement(By.PartialLinkText("Sauce Labs Back"));

        Assert.That(element.Text, Is.EqualTo("Sauce Labs Backpack"));
    }
}
