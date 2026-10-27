using OpenQA.Selenium;

namespace FinalSurge.Core.Pages;

public class CalendarPage : BasePage<CalendarPage>
{
    private readonly DateTime _date;

    public CalendarPage(IWebDriver driver, DateTime date) : base(driver)
    {
        _date = date;
    }

    protected override void ExecuteLoad()
    {
        Log.Info($"Open calendar for {_date:yyyy-MM-dd}");
        _driver.Navigate().GoToUrl($"{BaseUrl}/Calendar.cshtml?y={_date.Year}&m={_date.Month}&d={_date.Day}");
    }

    protected override bool EvaluateLoadedStatus() =>
        _driver.Title == "Final Surge - Training Calendar" &&
        _driver.Url.Contains($"y={_date.Year}&m={_date.Month}&d={_date.Day}");

    private static By WorkoutTitle(string name) =>
        By.XPath($"//div[contains(@class,'calendarworkout')]//div[contains(@class,'fc-event') and @data-title='{name}']");

    private static By WorkoutCard(string name) =>
        By.XPath($"//div[contains(@class,'calendarworkout')][.//div[contains(@class,'fc-event') and @data-title='{name}']]");

    public bool IsWorkoutDisplayed(string name)
    {
        try
        {
            Waiter.Until(_ => IsElementPresent(WorkoutTitle(name)));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public bool IsWorkoutRemoved(string name)
    {
        try
        {
            Waiter.Until(_ => !IsElementPresent(WorkoutTitle(name)));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public ConfirmationModal ClickDeleteWorkout(string name)
    {
        Log.Info($"Delete workout {name}");
        IWebElement card = _driver.FindElement(WorkoutCard(name));
        card.FindElement(By.CssSelector("a.dropdown-toggle")).Click();

        IWebElement deleteLink = card.FindElement(By.CssSelector("a.quick-delete"));
        Waiter.Until(_ => deleteLink.Displayed);
        deleteLink.Click();
        return new ConfirmationModal(_driver).Load();
    }

    public CalendarPage DeleteWorkout(string name)
    {
        ClickDeleteWorkout(name).Confirm();
        return this;
    }
}
