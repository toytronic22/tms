using OpenQA.Selenium;

namespace SauceDemoLocators;

[TestFixture]
public class XPathLocatorsTests : BaseTest
{
    [Test]
    public void ByAttribute_FindsPasswordInput()
    {
        var element = Driver.FindElement(By.XPath("//input[@type='password']"));

        Assert.That(element.GetAttribute("id"), Is.EqualTo("password"));
    }

    [Test]
    public void ByText_FindsAddToCartButton()
    {
        LogIn();

        var elements = Driver.FindElements(By.XPath("//button[text()='Add to cart']"));

        Assert.That(elements, Has.Count.EqualTo(6));
    }

    [Test]
    public void ByPartialAttribute_FindsAddToCartById()
    {
        LogIn();

        var element = Driver.FindElement(By.XPath("//button[contains(@id,'sauce-labs-backpack')]"));

        Assert.That(element.Text, Is.EqualTo("Add to cart"));
    }

    [Test]
    public void ByPartialText_FindsProductByPartOfName()
    {
        LogIn();

        var element = Driver.FindElement(By.XPath("//div[contains(text(),'Fleece')]"));

        Assert.That(element.Text, Is.EqualTo("Sauce Labs Fleece Jacket"));
    }

    [Test]
    public void Ancestor_FindsProductCardFromItsName()
    {
        LogIn();

        var card = Driver.FindElement(
            By.XPath("//div[text()='Sauce Labs Backpack']//ancestor::div[@class='inventory_item']"));

        Assert.That(card.Text, Does.Contain("$29.99"));
    }

    [Test]
    public void Descendant_FindsPriceInsideProductCard()
    {
        LogIn();

        var price = Driver.FindElement(
            By.XPath("//div[@class='inventory_item'][1]//descendant::div[@class='inventory_item_price']"));

        Assert.That(price.Text, Does.StartWith("$"));
    }

    [Test]
    public void Following_FindsPasswordInputAfterUsername()
    {
        var element = Driver.FindElement(By.XPath("//input[@id='user-name']//following::input[1]"));

        Assert.That(element.GetAttribute("id"), Is.EqualTo("password"));
    }

    [Test]
    public void Parent_FindsContainerOfProductName()
    {
        LogIn();

        var parent = Driver.FindElement(
            By.XPath("//div[text()='Sauce Labs Backpack']//parent::a"));

        Assert.That(parent.GetAttribute("id"), Is.EqualTo("item_4_title_link"));
    }

    [Test]
    public void Preceding_FindsUsernameInputBeforePassword()
    {
        var element = Driver.FindElement(By.XPath("//input[@id='password']//preceding::input[1]"));

        Assert.That(element.GetAttribute("id"), Is.EqualTo("user-name"));
    }

    [Test]
    public void AndCondition_FindsUsernameByTwoAttributes()
    {
        var element = Driver.FindElement(
            By.XPath("//input[@type='text' and @name='user-name']"));

        Assert.That(element.GetAttribute("placeholder"), Is.EqualTo("Username"));
    }
}
