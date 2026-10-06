using Allure.NUnit;
using SauceDemo.Core.Pages;

namespace SauceDemo.Tests;

[AllureNUnit]
public class ProductDetailsTests : BaseTest
{
    [Test]
    public void ProductDetailsShowSelectedProduct()
    {
        ProductDetailsPage detailsPage = new LoginPage(driver)
            .Login()
            .OpenBackpackDetails();

        Assert.Multiple(() =>
        {
            Assert.That(detailsPage.GetName(), Is.EqualTo("Sauce Labs Backpack"));
            Assert.That(detailsPage.GetPrice(), Is.EqualTo("$29.99"));
            Assert.That(detailsPage.GetUrl(), Does.Contain("/inventory-item.html?id=4"));
        });
    }

    [Test]
    public void BackToProductsReturnsToProductsList()
    {
        ProductsPage productsPage = new LoginPage(driver)
            .Login()
            .OpenBackpackDetails()
            .ClickBackToProducts();

        Assert.That(productsPage.IsProductsPageDisplayed(), Is.True);
    }
}
