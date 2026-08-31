using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoLocators;

public class LocatorsTests
{
    private const string BaseUrl = "https://www.saucedemo.com";
    private const string StandardUser = "standard_user";
    private const string Password = "secret_sauce";

    private IWebDriver _driver = null!;

    private readonly By _byId = By.Id("login-button");
    private readonly By _byName = By.Name("user-name");
    private readonly By _byClassName = By.ClassName("login_logo");
    private readonly By _byTagName = By.TagName("input");
    private readonly By _byLinkText = By.LinkText("Twitter");
    private readonly By _byPartialLinkText = By.PartialLinkText("Sauce Labs Back");

    private readonly By _xpathByAttribute = By.XPath("//input[@type='password']");
    private readonly By _xpathByText = By.XPath("//button[text()='Add to cart']");
    private readonly By _xpathContainsAttribute = By.XPath("//button[contains(@id,'sauce-labs-backpack')]");
    private readonly By _xpathContainsText = By.XPath("//div[contains(text(),'Fleece')]");
    private readonly By _xpathAncestor = By.XPath("//div[text()='Sauce Labs Backpack']//ancestor::div[@class='inventory_item']");
    private readonly By _xpathDescendant = By.XPath("//div[@class='inventory_item'][1]//descendant::div[@class='inventory_item_price']");
    private readonly By _xpathFollowing = By.XPath("//input[@id='user-name']//following::input[1]");
    private readonly By _xpathParent = By.XPath("//div[text()='Sauce Labs Backpack']//parent::a");
    private readonly By _xpathPreceding = By.XPath("//input[@id='password']//preceding::input[1]");
    private readonly By _xpathAnd = By.XPath("//input[@type='text' and @name='user-name']");

    private readonly By _cssClass = By.CssSelector(".inventory_item");
    private readonly By _cssTwoClasses = By.CssSelector(".btn_primary.btn_inventory");
    private readonly By _cssDescendantClass = By.CssSelector(".inventory_item .inventory_item_price");
    private readonly By _cssId = By.CssSelector("#login-button");
    private readonly By _cssTagName = By.CssSelector("select");
    private readonly By _cssTagWithClass = By.CssSelector("select.product_sort_container");
    private readonly By _cssAttributeEquals = By.CssSelector("[data-test=username]");
    private readonly By _cssAttributeContainsWord = By.CssSelector("[class~=btn_inventory]");
    private readonly By _cssAttributeStartsWithWord = By.CssSelector("[data-test|=add]");
    private readonly By _cssAttributeStartsWith = By.CssSelector("[id^=add-to-cart]");
    private readonly By _cssAttributeEndsWith = By.CssSelector("[id$=backpack]");
    private readonly By _cssAttributeContains = By.CssSelector("[id*=sauce-labs]");

    [SetUp]
    public void SetUp()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--guest");
        options.AddArgument("--start-maximized");

        _driver = new ChromeDriver(options);
        _driver.Manage().Window.Maximize();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        _driver.Navigate().GoToUrl(BaseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
        _driver.Dispose();
    }

    [Test]
    public void FindElementsByAllLocatorTypes()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_driver.FindElement(_byId).GetAttribute("value"), Is.EqualTo("Login"));
            Assert.That(_driver.FindElement(_byName).GetAttribute("placeholder"), Is.EqualTo("Username"));
            Assert.That(_driver.FindElement(_byClassName).Text, Is.EqualTo("Swag Labs"));
            Assert.That(_driver.FindElements(_byTagName), Has.Count.EqualTo(3));
            Assert.That(_driver.FindElement(_xpathByAttribute).GetAttribute("id"), Is.EqualTo("password"));
            Assert.That(_driver.FindElement(_xpathFollowing).GetAttribute("id"), Is.EqualTo("password"));
            Assert.That(_driver.FindElement(_xpathPreceding).GetAttribute("id"), Is.EqualTo("user-name"));
            Assert.That(_driver.FindElement(_xpathAnd).GetAttribute("placeholder"), Is.EqualTo("Username"));
            Assert.That(_driver.FindElement(_cssId).GetAttribute("value"), Is.EqualTo("Login"));
            Assert.That(_driver.FindElement(_cssAttributeEquals).GetAttribute("id"), Is.EqualTo("user-name"));
        });

        LogIn();

        Assert.Multiple(() =>
        {
            Assert.That(_driver.FindElement(_byLinkText).GetAttribute("href"), Does.Contain("twitter.com"));
            Assert.That(_driver.FindElement(_byPartialLinkText).Text, Is.EqualTo("Sauce Labs Backpack"));
            Assert.That(_driver.FindElements(_xpathByText), Has.Count.EqualTo(6));
            Assert.That(_driver.FindElement(_xpathContainsAttribute).Text, Is.EqualTo("Add to cart"));
            Assert.That(_driver.FindElement(_xpathContainsText).Text, Is.EqualTo("Sauce Labs Fleece Jacket"));
            Assert.That(_driver.FindElement(_xpathAncestor).Text, Does.Contain("$29.99"));
            Assert.That(_driver.FindElement(_xpathDescendant).Text, Does.StartWith("$"));
            Assert.That(_driver.FindElement(_xpathParent).GetAttribute("id"), Is.EqualTo("item_4_title_link"));
            Assert.That(_driver.FindElements(_cssClass), Has.Count.EqualTo(6));
            Assert.That(_driver.FindElements(_cssTwoClasses), Has.Count.EqualTo(6));
            Assert.That(_driver.FindElement(_cssDescendantClass).Text, Does.StartWith("$"));
            Assert.That(_driver.FindElement(_cssTagName).GetAttribute("class"), Is.EqualTo("product_sort_container"));
            Assert.That(_driver.FindElement(_cssTagWithClass).Displayed, Is.True);
            Assert.That(_driver.FindElements(_cssAttributeContainsWord), Has.Count.EqualTo(6));
            Assert.That(_driver.FindElements(_cssAttributeStartsWithWord), Is.Not.Empty);
            Assert.That(_driver.FindElements(_cssAttributeStartsWith), Has.Count.EqualTo(6));
            Assert.That(_driver.FindElement(_cssAttributeEndsWith).Text, Is.EqualTo("Add to cart"));
            Assert.That(_driver.FindElements(_cssAttributeContains), Is.Not.Empty);
        });
    }

    private void LogIn()
    {
        _driver.FindElement(By.Id("user-name")).SendKeys(StandardUser);
        _driver.FindElement(By.Id("password")).SendKeys(Password);
        _driver.FindElement(By.Id("login-button")).Click();
    }
}
