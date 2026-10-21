using OpenQA.Selenium;

namespace FinalSurge.Core.Elements;

public class Select2DropDown : BaseElement
{
    private readonly IWebDriver _driver;

    public Select2DropDown(IWebDriver driver, IWebElement element) : base(element)
    {
        _driver = driver;
    }

    public void SelectByText(string text)
    {
        _element.FindElement(By.CssSelector("a.select2-choice")).Click();
        _driver.FindElement(By.XPath(
            $"//div[contains(@class,'select2-drop-active')]//li[contains(@class,'select2-result-selectable')][normalize-space()='{text}']"))
            .Click();
    }

    public string GetSelectedText() => _element.FindElement(By.CssSelector("a.select2-choice span")).Text.Trim();
}
