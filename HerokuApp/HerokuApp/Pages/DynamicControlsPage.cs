using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HerokuApp.Pages;

public class DynamicControlsPage
{
    private readonly IWebDriver _driver;

    private readonly By _checkbox = By.CssSelector("#checkbox-example input[type='checkbox']");
    private readonly By _btnCheckbox = By.CssSelector("#checkbox-example button");
    private readonly By _input = By.CssSelector("#input-example input[type='text']");
    private readonly By _btnInput = By.CssSelector("#input-example button");
    private readonly By _message = By.Id("message");

    public DynamicControlsPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public DynamicControlsPage Open()
    {
        _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_controls");
        return this;
    }

    public bool IsCheckboxDisplayed() => _driver.FindElements(_checkbox).Count > 0;

    public bool IsInputEnabled() => _driver.FindElement(_input).Enabled;

    public DynamicControlsPage ClickCheckboxButton()
    {
        _driver.FindElement(_btnCheckbox).Click();
        return this;
    }

    public DynamicControlsPage ClickInputButton()
    {
        _driver.FindElement(_btnInput).Click();
        return this;
    }

    public DynamicControlsPage WaitForMessage(string expectedText)
    {
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElements(_message).Count > 0 && d.FindElement(_message).Text == expectedText);
        return this;
    }

    public string GetMessage() => _driver.FindElement(_message).Text;
}
