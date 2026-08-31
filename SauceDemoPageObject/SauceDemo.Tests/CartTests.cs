using SauceDemo.Core.Pages;

namespace SauceDemo.Tests;

public class CartTests : BaseTest
{
    [Test]
    public void AddProductToCart()
    {
        ProductsPage productsPage = new LoginPage(driver).Login();
        productsPage.AddBackpackToCart();

        Assert.Multiple(() =>
        {
            Assert.That(productsPage.Header.GetCartCounter(), Is.EqualTo(1));
            Assert.That(productsPage.IsRemoveButtonDisplayed(), Is.True);
        });
    }

    [Test]
    public void AddedProductIsShownInCart()
    {
        ProductsPage productsPage = new LoginPage(driver).Login();
        CartPage cartPage = productsPage.AddBackpackToCart().Header.OpenCart();

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
        ProductsPage productsPage = new LoginPage(driver).Login();
        CartPage cartPage = productsPage.AddBackpackToCart().Header.OpenCart();
        cartPage.RemoveBackpack();

        Assert.Multiple(() =>
        {
            Assert.That(cartPage.IsEmpty(), Is.True);
            Assert.That(cartPage.Header.GetCartCounter(), Is.EqualTo(0));
        });
    }
}
