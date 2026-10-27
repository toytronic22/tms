using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FinalSurge.Core.Elements;

public class DropDown : BaseElement
{
    public DropDown(IWebElement element) : base(element)
    {
    }

    public void SelectByText(string text) => new SelectElement(_element).SelectByText(text);

    public void SelectByValue(string value) => new SelectElement(_element).SelectByValue(value);

    public string GetSelectedText() => new SelectElement(_element).SelectedOption.Text.Trim();
}
