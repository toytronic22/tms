using OpenQA.Selenium;

namespace SauceDemo.Core.Elements;

public class Link : BaseElement
{
    public Link(IWebElement element) : base(element)
    {
    }

    public void Click() => _element.Click();
}
