using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace HerokuApp.com.demoqa.Pages;

public class DroppablePage
{
    private readonly IWebDriver _driver;

    private readonly By _dragMe = By.CssSelector("#simpleDropContainer #draggable");
    private readonly By _dropHere = By.CssSelector("#simpleDropContainer #droppable");

    public DroppablePage(IWebDriver driver)
    {
        _driver = driver;
    }

    public DroppablePage Open()
    {
        _driver.Navigate().GoToUrl("https://demoqa.com/droppable");
        WaitUntilDragAndDropIsReady();
        return this;
    }

    public DroppablePage DragMeToDropHere()
    {
        IWebElement source = _driver.FindElement(_dragMe);
        IWebElement target = _driver.FindElement(_dropHere);

        new Actions(_driver)
            .ClickAndHold(source)
            .MoveByOffset(10, 10)
            .MoveToElement(target)
            .Pause(TimeSpan.FromMilliseconds(300))
            .Release()
            .Perform();

        return this;
    }

    public DroppablePage WaitForDroppedText()
    {
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        wait.Until(d => d.FindElement(_dropHere).Text == "Dropped!");
        return this;
    }

    public string GetDropAreaText() => _driver.FindElement(_dropHere).Text;

    private void WaitUntilDragAndDropIsReady()
    {
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        wait.Until(d =>
        {
            string dragClass = d.FindElement(_dragMe).GetAttribute("class") ?? string.Empty;
            return dragClass.Contains("ui-draggable");
        });
    }
}
