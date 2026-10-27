using Booking.Tests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace Booking.Tests.Steps;

[Binding]
public class HotelSearchSteps
{
    private readonly SearchResultsPage _searchResultsPage;

    public HotelSearchSteps(IWebDriver driver)
    {
        _searchResultsPage = new SearchResultsPage(driver);
    }

    [Given("the search results page is open")]
    public void GivenTheSearchResultsPageIsOpen()
    {
        _searchResultsPage.Open();
    }

    [When("I search for {string}")]
    public void WhenISearchFor(string hotelName)
    {
        _searchResultsPage.SearchHotel(hotelName);
    }

    [Then("the hotel {string} is shown on the page")]
    public void ThenTheHotelIsShownOnThePage(string hotelName)
    {
        Assert.That(_searchResultsPage.IsHotelDisplayed(hotelName), Is.True);
    }

    [Then("the hotel {string} has rating {string}")]
    public void ThenTheHotelHasRating(string hotelName, string rating)
    {
        Assert.That(_searchResultsPage.GetHotelRating(hotelName), Is.EqualTo(rating));
    }
}
