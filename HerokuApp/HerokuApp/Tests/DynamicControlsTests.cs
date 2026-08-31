using HerokuApp.Pages;

namespace HerokuApp.Tests;

public class DynamicControlsTests : BaseTest
{
    [Test]
    public void CheckboxIsRemovedAndInputBecomesEnabled()
    {
        DynamicControlsPage page = new DynamicControlsPage(driver).Open();

        Assert.That(page.IsCheckboxDisplayed(), Is.True);

        page.ClickCheckboxButton().WaitForMessage("It's gone!");

        Assert.Multiple(() =>
        {
            Assert.That(page.GetMessage(), Is.EqualTo("It's gone!"));
            Assert.That(page.IsCheckboxDisplayed(), Is.False);
            Assert.That(page.IsInputEnabled(), Is.False);
        });

        page.ClickInputButton().WaitForMessage("It's enabled!");

        Assert.Multiple(() =>
        {
            Assert.That(page.GetMessage(), Is.EqualTo("It's enabled!"));
            Assert.That(page.IsInputEnabled(), Is.True);
        });
    }
}
