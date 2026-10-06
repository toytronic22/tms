using OpenQA.Selenium;
using SauceDemo.Core.Elements;

namespace SauceDemo.Core.Pages;

public class CheckoutInformationPage : BasePage
{
    private readonly By _inpFirstName = By.Id("first-name");
    private readonly By _inpLastName = By.Id("last-name");
    private readonly By _inpPostalCode = By.Id("postal-code");
    private readonly By _btnContinue = By.Id("continue");
    private readonly By _msgError = By.CssSelector("h3[data-test='error']");

    private Input FirstNameInput => new(_driver.FindElement(_inpFirstName));
    private Input LastNameInput => new(_driver.FindElement(_inpLastName));
    private Input PostalCodeInput => new(_driver.FindElement(_inpPostalCode));
    private Button ContinueButton => new(_driver.FindElement(_btnContinue));
    private Label ErrorMessage => new(_driver.FindElement(_msgError));

    public CheckoutInformationPage(IWebDriver driver) : base(driver)
    {
    }

    public CheckoutInformationPage SetFirstName(string firstName)
    {
        FirstNameInput.SetText(firstName);
        return this;
    }

    public CheckoutInformationPage SetLastName(string lastName)
    {
        LastNameInput.SetText(lastName);
        return this;
    }

    public CheckoutInformationPage SetPostalCode(string postalCode)
    {
        PostalCodeInput.SetText(postalCode);
        return this;
    }

    public CheckoutOverviewPage ClickContinue()
    {
        ContinueButton.Click();
        return new CheckoutOverviewPage(_driver).WaitUntilOpened();
    }

    public CheckoutInformationPage ClickContinueExpectingError()
    {
        ContinueButton.Click();
        return this;
    }

    public CheckoutOverviewPage FillInformation(string firstName = "Alexey", string lastName = "Martynov", string postalCode = "220000")
    {
        Log.Info($"Fill checkout information: {firstName} {lastName}, {postalCode}");
        return SetFirstName(firstName).SetLastName(lastName).SetPostalCode(postalCode).ClickContinue();
    }

    public string GetErrorMessage()
    {
        Wait().Until(_ => ErrorMessage.IsDisplayed());
        return ErrorMessage.GetText();
    }
}
