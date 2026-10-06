using Allure.NUnit;
using SauceDemo.Core.Pages;

namespace SauceDemo.Tests;

[AllureNUnit]
public class CartTests : BaseTest
{
    [Test]
    public void AddedProductIsShownInCart()
    {
        CartPage cartPage = new LoginPage(driver)
            .Login()
            .AddBackpackToCart()
            .Header.OpenCart();

        Assert.Multiple(() =>
        {
            Assert.That(cartPage.GetTitle(), Is.EqualTo("Your Cart"));
            Assert.That(cartPage.GetItemsCount(), Is.EqualTo(1));
            Assert.That(cartPage.GetFirstItemName(), Is.EqualTo("Sauce Labs Backpack"));
            Assert.That(cartPage.GetFirstItemQuantity(), Is.EqualTo("1"));
        });
    }

    [Test]
    public void RemoveProductFromCart()
    {
        CartPage cartPage = new LoginPage(driver)
            .Login()
            .AddBackpackToCart()
            .Header.OpenCart()
            .RemoveBackpack();

        Assert.Multiple(() =>
        {
            Assert.That(cartPage.IsEmpty(), Is.True);
            Assert.That(cartPage.Header.GetCartCounter(), Is.EqualTo(0));
        });
    }

    [Test]
    public void ResetAppStateClearsCart()
    {
        ProductsPage productsPage = new LoginPage(driver).Login().AddBackpackToCart();
        productsPage.Header.OpenSideMenu().ResetAppState();

        Assert.That(productsPage.Header.GetCartCounter(), Is.EqualTo(0));
    }
}
