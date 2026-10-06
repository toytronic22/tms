using OpenQA.Selenium;

namespace SauceDemo.Core.Elements;

public class BaseElement
{
    protected readonly IWebElement _element;

    public BaseElement(IWebElement element)
    {
        _element = element;
    }

    public string GetText() => _element.Text;

    public bool IsDisplayed() => _element.Displayed;
}
