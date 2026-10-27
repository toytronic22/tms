using OpenQA.Selenium;

namespace FinalSurge.Core.Elements;

public class Button : BaseElement
{
    public Button(IWebElement element) : base(element)
    {
    }

    public void Click() => _element.Click();
}
