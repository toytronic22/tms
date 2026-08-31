using SauceDemo.Core.Pages;

namespace SauceDemo.Tests;

public class CheckoutTests : BaseTest
{
    [Test]
    public void CheckoutWithoutFirstNameShowsError()
    {
        CheckoutInformationPage informationPage = new LoginPage(driver)
            .Login()
            .AddBackpackToCart()
            .Header.OpenCart()
            .ClickCheckout();

        informationPage.ClickContinueExpectingError();

        Assert.That(informationPage.GetErrorMessage(), Is.EqualTo("Error: First Name is required"));
    }

    [Test]
    public void CheckoutOverviewShowsCorrectTotal()
    {
        CheckoutOverviewPage overviewPage = new LoginPage(driver)
            .Login()
            .AddBackpackToCart()
            .Header.OpenCart()
            .ClickCheckout()
            .FillInformation();

        Assert.Multiple(() =>
        {
            Assert.That(overviewPage.GetTitle(), Is.EqualTo("Checkout: Overview"));
            Assert.That(overviewPage.GetItemsCount(), Is.EqualTo(1));
            Assert.That(overviewPage.GetItemTotal(), Is.EqualTo(29.99m));
            Assert.That(overviewPage.GetTotal(), Is.EqualTo(overviewPage.GetItemTotal() + overviewPage.GetTax()));
        });
    }

    [Test]
    public void CompleteOrderSuccessfully()
    {
        CheckoutCompletePage completePage = new LoginPage(driver)
            .Login()
            .AddBackpackToCart()
            .Header.OpenCart()
            .ClickCheckout()
            .FillInformation()
            .ClickFinish();

        Assert.Multiple(() =>
        {
            Assert.That(completePage.IsOrderCompleted(), Is.True);
            Assert.That(completePage.GetCompleteHeader(), Is.EqualTo("Thank you for your order!"));
            Assert.That(completePage.GetUrl(), Does.Contain("/checkout-complete.html"));
        });
    }
}
