using FinalSurge.Core.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FinalSurge.Core.Pages;

public class ConfirmationModal : BasePage<ConfirmationModal>
{
    [FindsBy(How = How.CssSelector, Using = ".bootbox.in .modal-body")]
    private IWebElement _lblMessage = null!;

    [FindsBy(How = How.CssSelector, Using = ".bootbox.in a[data-handler='1']")]
    private IWebElement _btnOk = null!;

    [FindsBy(How = How.CssSelector, Using = ".bootbox.in a[data-handler='0']")]
    private IWebElement _btnCancel = null!;

    private int _lastButtonTop = int.MinValue;

    private Label MessageLabel => new(_lblMessage);
    private Button OkButton => new(_btnOk);
    private Button CancelButton => new(_btnCancel);

    public ConfirmationModal(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
    }

    protected override bool EvaluateLoadedStatus()
    {
        if (!OkButton.IsDisplayed())
        {
            return false;
        }

        int buttonTop = _btnOk.Location.Y;
        bool isAnimationFinished = buttonTop == _lastButtonTop;
        _lastButtonTop = buttonTop;
        return isAnimationFinished;
    }

    public string GetMessage() => MessageLabel.GetText();

    public void Confirm()
    {
        Log.Info($"Confirm: {GetMessage()}");
        OkButton.Click();
    }

    public void Cancel()
    {
        Log.Info($"Cancel: {GetMessage()}");
        CancelButton.Click();
        Waiter.Until(_ => !IsElementPresent(By.CssSelector(".bootbox.in")));
    }
}
