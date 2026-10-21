using OpenQA.Selenium;

namespace FinalSurge.Core.Elements;

public class RadioButton : BaseElement
{
    public RadioButton(IWebElement element) : base(element)
    {
    }

    public void Select()
    {
        if (!_element.Selected)
        {
            _element.Click();
        }
    }
}
