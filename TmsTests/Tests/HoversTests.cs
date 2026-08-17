using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TmsTests.Tests;

[TestFixture]
public class HoversTests : BaseTest
{
    private static readonly By Figures = By.ClassName("figure");

    [Test]
    public void HoverOverEachProfile_ShowsNameAndLinkToUserPage()
    {
        OpenPage("/hovers");
        var figuresCount = Driver.FindElements(Figures).Count;

        Assert.That(figuresCount, Is.EqualTo(3));

        for (var index = 0; index < figuresCount; index++)
        {
            OpenPage("/hovers");
            var figure = Driver.FindElements(Figures)[index];

            new Actions(Driver).MoveToElement(figure).Perform();

            var caption = figure.FindElement(By.ClassName("figcaption"));
            var name = caption.FindElement(By.TagName("h5")).Text;
            Assert.That(name, Is.EqualTo($"name: user{index + 1}"));

            caption.FindElement(By.TagName("a")).Click();
            Assert.That(Driver.Url, Does.EndWith($"/users/{index + 1}"));
        }
    }

    [Test]
    public void ProfilePages_ShouldNotReturnNotFound()
    {
        OpenPage("/hovers");
        var figuresCount = Driver.FindElements(Figures).Count;

        Assert.Multiple(() =>
        {
            for (var index = 0; index < figuresCount; index++)
            {
                OpenPage($"/users/{index + 1}");
                var pageText = Driver.FindElement(By.TagName("body")).Text;

                Assert.That(pageText, Does.Not.Contain("Not Found"));
            }
        });
    }
}
