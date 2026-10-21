using System.Globalization;
using FinalSurge.Core.Elements;
using FinalSurge.Core.Models;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FinalSurge.Core.Pages;

public class ShoesPage : BasePage<ShoesPage>
{
    [FindsBy(How = How.Id, Using = "ShoeName")]
    private IWebElement _inpName = null!;

    [FindsBy(How = How.Id, Using = "ShoeBrand")]
    private IWebElement _ddlBrand = null!;

    [FindsBy(How = How.Id, Using = "ShoeModel")]
    private IWebElement _inpModel = null!;

    [FindsBy(How = How.Id, Using = "ShoeCost")]
    private IWebElement _inpCost = null!;

    [FindsBy(How = How.Id, Using = "ShoeSize")]
    private IWebElement _ddlSize = null!;

    [FindsBy(How = How.Id, Using = "DistAlert")]
    private IWebElement _inpAlertDistance = null!;

    [FindsBy(How = How.Id, Using = "DistAlertType")]
    private IWebElement _ddlAlertDistanceUnit = null!;

    [FindsBy(How = How.Id, Using = "saveButton")]
    private IWebElement _btnSave = null!;

    [FindsBy(How = How.Id, Using = "del-shoe")]
    private IWebElement _btnDelete = null!;

    private Input NameInput => new(_inpName);
    private DropDown BrandDropDown => new(_ddlBrand);
    private Input ModelInput => new(_inpModel);
    private Input CostInput => new(_inpCost);
    private DropDown SizeDropDown => new(_ddlSize);
    private Input AlertDistanceInput => new(_inpAlertDistance);
    private DropDown AlertDistanceUnitDropDown => new(_ddlAlertDistanceUnit);
    private Button SaveButton => new(_btnSave);
    private Button DeleteButton => new(_btnDelete);

    public ShoesPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
        Log.Info("Open shoes page");
        _driver.Navigate().GoToUrl($"{BaseUrl}/EquipmentShoes.cshtml");
    }

    protected override bool EvaluateLoadedStatus() =>
        _driver.Url.Contains("EquipmentShoes.cshtml") && SaveButton.IsDisplayed();

    private static By ShoeLink(string name) => By.XPath($"//strong/a[normalize-space()='{name}']");

    public ShoesPage FillShoe(Shoe shoe)
    {
        NameInput.SetText(shoe.Name);
        BrandDropDown.SelectByText(shoe.Brand);
        ModelInput.SetText(shoe.Model);
        CostInput.SetText(shoe.Cost.ToString(CultureInfo.InvariantCulture));
        SizeDropDown.SelectByValue(shoe.Size);
        AlertDistanceInput.SetText(shoe.AlertDistance.ToString());
        AlertDistanceUnitDropDown.SelectByValue(shoe.AlertDistanceUnit);
        return this;
    }

    public ShoesPage AddShoe(Shoe shoe)
    {
        Log.Info($"Add shoe {shoe.Name}");
        FillShoe(shoe);
        SaveButton.Click();
        WaitForReload(_btnSave);
        return new ShoesPage(_driver).Load();
    }

    public bool IsShoeDisplayed(string name) => IsElementPresent(ShoeLink(name));

    public string GetShoeInfo(string name) =>
        _driver.FindElement(ShoeLink(name)).FindElement(By.XPath("ancestor::td[1]")).Text;

    public ShoesPage OpenShoe(string name)
    {
        Log.Info($"Open shoe {name}");
        _driver.FindElement(ShoeLink(name)).Click();
        Waiter.Until(_ => _driver.Url.Contains("shoe="));
        return new ShoesPage(_driver).Load();
    }

    public string GetName() => NameInput.GetValue();

    public string GetModel() => ModelInput.GetValue();

    public string GetBrand() => BrandDropDown.GetSelectedText();

    public ConfirmationModal ClickDelete()
    {
        DeleteButton.Click();
        return new ConfirmationModal(_driver).Load();
    }

    public ShoesPage DeleteShoe(string name)
    {
        Log.Info($"Delete shoe {name}");
        OpenShoe(name).ClickDelete().Confirm();
        Waiter.Until(_ => _driver.Url.Contains("delete="));
        return new ShoesPage(_driver).Load();
    }
}
