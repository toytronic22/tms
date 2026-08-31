using OpenQA.Selenium;

namespace SauceDemo.Core.Pages;

public class CheckoutInformationPage : BasePage
{
    private readonly By _inpFirstName = By.Id("first-name");
    private readonly By _inpLastName = By.Id("last-name");
    private readonly By _inpPostalCode = By.Id("postal-code");
    private readonly By _btnContinue = By.Id("continue");
    private readonly By _msgError = By.CssSelector("h3[data-test='error']");

    public CheckoutInformationPage(IWebDriver driver) : base(driver)
    {
    }

    public CheckoutInformationPage SetFirstName(string firstName)
    {
        _driver.FindElement(_inpFirstName).SendKeys(firstName);
        return this;
    }

    public CheckoutInformationPage SetLastName(string lastName)
    {
        _driver.FindElement(_inpLastName).SendKeys(lastName);
        return this;
    }

    public CheckoutInformationPage SetPostalCode(string postalCode)
    {
        _driver.FindElement(_inpPostalCode).SendKeys(postalCode);
        return this;
    }

    public CheckoutOverviewPage ClickContinue()
    {
        _driver.FindElement(_btnContinue).Click();
        return new CheckoutOverviewPage(_driver);
    }

    public CheckoutInformationPage ClickContinueExpectingError()
    {
        _driver.FindElement(_btnContinue).Click();
        return this;
    }

    public CheckoutOverviewPage FillInformation(string firstName = "Alexey", string lastName = "Martynov", string postalCode = "220000") =>
        SetFirstName(firstName).SetLastName(lastName).SetPostalCode(postalCode).ClickContinue();

    public string GetErrorMessage()
    {
        Wait().Until(d => d.FindElement(_msgError).Displayed);
        return _driver.FindElement(_msgError).Text;
    }
}
