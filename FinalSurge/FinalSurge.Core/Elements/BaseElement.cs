using OpenQA.Selenium;

namespace FinalSurge.Core.Elements;

public class BaseElement
{
    protected readonly IWebElement _element;

    public BaseElement(IWebElement element)
    {
        _element = element;
    }

    public string GetText() => _element.Text.Trim();

    public bool IsDisplayed()
    {
        try
        {
            return _element.Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}
