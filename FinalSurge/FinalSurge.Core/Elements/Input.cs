using OpenQA.Selenium;

namespace FinalSurge.Core.Elements;

public class Input : BaseElement
{
    public Input(IWebElement element) : base(element)
    {
    }

    public void SetText(string text)
    {
        _element.Clear();
        _element.SendKeys(text);
    }

    public string GetValue() => _element.GetAttribute("value") ?? string.Empty;
}
