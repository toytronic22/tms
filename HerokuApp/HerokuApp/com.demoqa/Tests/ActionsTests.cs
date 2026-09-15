using HerokuApp.com.demoqa.Pages;

namespace HerokuApp.com.demoqa.Tests;

public class ActionsTests : BaseTest
{
    [Test]
    public void DragAndDropShowsDroppedText()
    {
        DroppablePage page = new DroppablePage(driver).Open();

        page.DragMeToDropHere().WaitForDroppedText();

        Assert.That(page.GetDropAreaText(), Is.EqualTo("Dropped!"));
    }
}
