using OpenQA.Selenium;

namespace SauceDemoLocators;

[TestFixture]
public class CssLocatorsTests : BaseTest
{
    [Test]
    public void ByClass_FindsAllProductCards()
    {
        LogIn();

        var elements = Driver.FindElements(By.CssSelector(".inventory_item"));

        Assert.That(elements, Has.Count.EqualTo(6));
    }

    [Test]
    public void ByTwoClassesOnSameElement_FindsAddToCartButtons()
    {
        LogIn();

        var elements = Driver.FindElements(By.CssSelector(".btn_primary.btn_inventory"));

        Assert.That(elements, Has.Count.EqualTo(6));
    }

    [Test]
    public void ByDescendantClass_FindsPriceInsideCard()
    {
        LogIn();

        var element = Driver.FindElement(By.CssSelector(".inventory_item .inventory_item_price"));

        Assert.That(element.Text, Does.StartWith("$"));
    }

    [Test]
    public void ById_FindsLoginButton()
    {
        var element = Driver.FindElement(By.CssSelector("#login-button"));

        Assert.That(element.GetAttribute("value"), Is.EqualTo("Login"));
    }

    [Test]
    public void ByTagName_FindsSelectOnInventory()
    {
        LogIn();

        var element = Driver.FindElement(By.CssSelector("select"));

        Assert.That(element.GetAttribute("class"), Is.EqualTo("product_sort_container"));
    }

    [Test]
    public void ByTagAndClass_FindsSortDropdown()
    {
        LogIn();

        var element = Driver.FindElement(By.CssSelector("select.product_sort_container"));

        Assert.That(element.Displayed, Is.True);
    }

    [Test]
    public void ByAttributeEquals_FindsUsernameInput()
    {
        var element = Driver.FindElement(By.CssSelector("[data-test=username]"));

        Assert.That(element.GetAttribute("id"), Is.EqualTo("user-name"));
    }

    [Test]
    public void ByAttributeContainsWord_FindsButtonByOneOfItsClasses()
    {
        LogIn();

        var elements = Driver.FindElements(By.CssSelector("[class~=btn_inventory]"));

        Assert.That(elements, Has.Count.EqualTo(6));
    }

    [Test]
    public void ByAttributeStartsWithWordAndHyphen_FindsAddToCartButton()
    {
        LogIn();

        var elements = Driver.FindElements(By.CssSelector("[data-test|=add]"));

        Assert.That(elements, Is.Not.Empty);
    }

    [Test]
    public void ByAttributeStartsWith_FindsAllAddToCartButtons()
    {
        LogIn();

        var elements = Driver.FindElements(By.CssSelector("[id^=add-to-cart]"));

        Assert.That(elements, Has.Count.EqualTo(6));
    }

    [Test]
    public void ByAttributeEndsWith_FindsBackpackButton()
    {
        LogIn();

        var element = Driver.FindElement(By.CssSelector("[id$=backpack]"));

        Assert.That(element.Text, Is.EqualTo("Add to cart"));
    }

    [Test]
    public void ByAttributeContains_FindsElementsWithSauceLabsInId()
    {
        LogIn();

        var elements = Driver.FindElements(By.CssSelector("[id*=sauce-labs]"));

        Assert.That(elements, Is.Not.Empty);
    }
}
