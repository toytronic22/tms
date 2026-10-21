using Allure.NUnit;
using Allure.NUnit.Attributes;
using FinalSurge.Core.Models;
using FinalSurge.Core.Pages;
using FinalSurge.Core.Utils;

namespace FinalSurge.Tests.Ui;

[AllureNUnit]
[AllureEpic("UI")]
[AllureFeature("Gear")]
[Category("UI")]
public class ShoesTests : BaseTest
{
    private Shoe _shoe = null!;

    [SetUp]
    public void CreateShoeData()
    {
        _shoe = TestDataGenerator.RunningShoe();
    }

    protected override void CleanUp()
    {
        ShoesPage shoesPage = new ShoesPage(driver).Load();

        if (shoesPage.IsShoeDisplayed(_shoe.Name))
        {
            shoesPage.DeleteShoe(_shoe.Name);
        }
    }

    [Test]
    [Category("Smoke")]
    public void AddShoe()
    {
        LoginAsDefaultUser();

        ShoesPage shoesPage = new ShoesPage(driver).Load().AddShoe(_shoe);

        Assert.That(shoesPage.IsShoeDisplayed(_shoe.Name), Is.True);
        Assert.That(shoesPage.GetShoeInfo(_shoe.Name), Does.Contain($"({_shoe.Brand} {_shoe.Model})")
            .And.Contain("Cost: $120.00")
            .And.Contain($"Size: {_shoe.Size}")
            .And.Contain("Alert at: 500 km"));
    }

    [Test]
    [Category("Regression")]
    public void OpenShoeShowsSavedData()
    {
        LoginAsDefaultUser();

        ShoesPage shoesPage = new ShoesPage(driver).Load()
            .AddShoe(_shoe)
            .OpenShoe(_shoe.Name);

        Assert.Multiple(() =>
        {
            Assert.That(shoesPage.GetName(), Is.EqualTo(_shoe.Name));
            Assert.That(shoesPage.GetBrand(), Is.EqualTo(_shoe.Brand));
            Assert.That(shoesPage.GetModel(), Is.EqualTo(_shoe.Model));
        });
    }

    [Test]
    [Category("Regression")]
    public void DeleteShoe()
    {
        LoginAsDefaultUser();

        ShoesPage shoesPage = new ShoesPage(driver).Load()
            .AddShoe(_shoe)
            .DeleteShoe(_shoe.Name);

        Assert.That(shoesPage.IsShoeDisplayed(_shoe.Name), Is.False);
    }
}
