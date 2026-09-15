using OpenQA.Selenium;

namespace TmsTests.Tests;

[TestFixture]
public class TablesTests : BaseTest
{
    [Test]
    public void Table1_Cells_ContainExpectedValues()
    {
        OpenPage("/tables");

        var lastName = Driver.FindElement(By.XPath("//table[@id='table1']//tbody//tr[1]//td[1]")).Text;
        var firstName = Driver.FindElement(By.XPath("//table[@id='table1']//tbody//tr[1]//td[2]")).Text;
        var email = Driver.FindElement(By.XPath("//table[@id='table1']//tbody//tr[1]//td[3]")).Text;
        var dueSecondRow = Driver.FindElement(By.XPath("//table[@id='table1']//tbody//tr[2]//td[4]")).Text;
        var webSiteThirdRow = Driver.FindElement(By.XPath("//table[@id='table1']//tbody//tr[3]//td[5]")).Text;

        Assert.Multiple(() =>
        {
            Assert.That(lastName, Is.EqualTo("Smith"));
            Assert.That(firstName, Is.EqualTo("John"));
            Assert.That(email, Is.EqualTo("jsmith@gmail.com"));
            Assert.That(dueSecondRow, Is.EqualTo("$51.00"));
            Assert.That(webSiteThirdRow, Is.EqualTo("http://www.jdoe.com"));
        });
    }
}
