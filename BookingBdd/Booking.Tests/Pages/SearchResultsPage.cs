using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Booking.Tests.Pages;

public class SearchResultsPage
{
    private const string Url = "https://www.booking.com/searchresults.en-gb.html";

    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By _rejectCookiesButton = By.Id("onetrust-reject-all-handler");
    private readonly By _dismissSignInButton = By.CssSelector("button[aria-label='Dismiss sign-in info.']");
    private readonly By _searchInput = By.Name("ss");
    private readonly By _firstSuggestion = By.CssSelector("[data-testid='autocomplete-result']");
    private readonly By _searchButton = By.CssSelector("button[type='submit']");
    private readonly By _hotelCards = By.CssSelector("[data-testid='property-card']");
    private readonly By _hotelTitle = By.CssSelector("[data-testid='title']");
    private readonly By _hotelRating = By.CssSelector("[data-testid='review-score'] > div:nth-child(2)");

    public SearchResultsPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        _wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
    }

    public SearchResultsPage Open()
    {
        _driver.Navigate().GoToUrl(Url);
        ClickIfAppears(_rejectCookiesButton);
        return this;
    }

    public SearchResultsPage SearchHotel(string hotelName)
    {
        ClickIfAppears(_dismissSignInButton);

        IWebElement searchInput = _wait.Until(driver => driver.FindElement(_searchInput));
        searchInput.Clear();
        searchInput.SendKeys(hotelName);

        IWebElement suggestion = _wait.Until(driver =>
        {
            IWebElement firstSuggestion = driver.FindElement(_firstSuggestion);
            return firstSuggestion.Text.Contains(hotelName) ? firstSuggestion : null;
        });
        suggestion.Click();

        string previousUrl = _driver.Url;
        _driver.FindElement(_searchButton).Click();

        _wait.Until(driver => driver.Url != previousUrl && driver.FindElements(_hotelCards).Count > 0);
        return this;
    }

    public bool IsHotelDisplayed(string hotelName)
    {
        try
        {
            _wait.Until(driver => FindHotelCard(hotelName) != null);
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public string GetHotelRating(string hotelName)
    {
        return _wait.Until(driver => FindHotelCard(hotelName)?.FindElement(_hotelRating).Text);
    }

    private IWebElement? FindHotelCard(string hotelName)
    {
        return _driver.FindElements(_hotelCards)
            .FirstOrDefault(card => card.FindElement(_hotelTitle).Text == hotelName);
    }

    private void ClickIfAppears(By locator)
    {
        try
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(5))
                .Until(driver => driver.FindElement(locator))
                .Click();
        }
        catch (WebDriverTimeoutException)
        {
        }
    }
}
