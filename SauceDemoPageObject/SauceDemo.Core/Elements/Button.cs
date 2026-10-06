using OpenQA.Selenium;

namespace SauceDemo.Core.Elements;

public class Button : BaseElement
{
    public Button(IWebElement element) : base(element)
    {
    }

    public void Click() => _element.Click();
}
