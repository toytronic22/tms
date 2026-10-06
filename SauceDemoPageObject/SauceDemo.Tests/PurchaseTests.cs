using Allure.NUnit;
using SauceDemo.Core.Pages;

namespace SauceDemo.Tests;

[AllureNUnit]
public class PurchaseTests : BaseTest
{
    [Test]
    public void BuyProductFromDetailsPage()
    {
        ProductDetailsPage detailsPage = new LoginPage(driver)
            .Login()
            .OpenBackpackDetails()
            .AddToCart();

        Assert.That(detailsPage.Header.GetCartCounter(), Is.EqualTo(1));

        CartPage cartPage = detailsPage.Header.OpenCart();

        Assert.That(cartPage.GetFirstItemName(), Is.EqualTo("Sauce Labs Backpack"));

        CheckoutOverviewPage overviewPage = cartPage
            .ClickCheckout()
            .FillInformation();

        Assert.Multiple(() =>
        {
            Assert.That(overviewPage.GetFirstItemName(), Is.EqualTo("Sauce Labs Backpack"));
            Assert.That(overviewPage.GetTotalText(), Is.EqualTo("Total: $32.39"));
        });

        CheckoutCompletePage completePage = overviewPage.ClickFinish();

        Assert.That(completePage.GetCompleteHeader(), Is.EqualTo("Thank you for your order!"));

        ProductsPage productsPage = completePage.ClickBackHome();

        Assert.Multiple(() =>
        {
            Assert.That(productsPage.IsProductsPageDisplayed(), Is.True);
            Assert.That(productsPage.Header.GetCartCounter(), Is.EqualTo(0));
        });
    }
}
