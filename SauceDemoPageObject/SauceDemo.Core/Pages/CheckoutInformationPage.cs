using OpenQA.Selenium;
using SauceDemo.Core.Elements;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Core.Pages;

public class CheckoutInformationPage : BasePage<CheckoutInformationPage>
{
    [FindsBy(How = How.Id, Using = "first-name")]
    private IWebElement _inpFirstName = null!;

    [FindsBy(How = How.Id, Using = "last-name")]
    private IWebElement _inpLastName = null!;

    [FindsBy(How = How.Id, Using = "postal-code")]
    private IWebElement _inpPostalCode = null!;

    [FindsBy(How = How.Id, Using = "continue")]
    private IWebElement _btnContinue = null!;

    [FindsBy(How = How.CssSelector, Using = "h3[data-test='error']")]
    private IWebElement _msgError = null!;

    private Input FirstNameInput => new(_inpFirstName);
    private Input LastNameInput => new(_inpLastName);
    private Input PostalCodeInput => new(_inpPostalCode);
    private Button ContinueButton => new(_btnContinue);
    private Label ErrorMessage => new(_msgError);

    public CheckoutInformationPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad() => _driver.Navigate().GoToUrl($"{BaseUrl}/checkout-step-one.html");

    protected override bool EvaluateLoadedStatus() =>
        _driver.Url.Contains("/checkout-step-one.html") && ContinueButton.IsDisplayed();

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
        return new CheckoutOverviewPage(_driver).Load();
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

    public string GetErrorMessage() => ErrorMessage.GetText();
}
