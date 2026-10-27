using System.Net;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using FinalSurge.Core.Api;
using FinalSurge.Core.Api.Clients;
using FinalSurge.Core.Api.Models;
using FinalSurge.Core.Utils;
using RestSharp;

namespace FinalSurge.Tests.Api;

[AllureNUnit]
[AllureEpic("API")]
[AllureFeature("Calendar labels")]
[Category("API")]
public class CalendarLabelApiTests
{
    private static readonly DateTime StartDate = DateTime.Today;
    private static readonly DateTime EndDate = DateTime.Today.AddDays(2);

    private CalendarLabelClient _client = null!;
    private readonly List<string> _createdLabelKeys = new();

    [SetUp]
    public void Setup()
    {
        _client = ApiSession.CalendarLabels;
    }

    [TearDown]
    public void DeleteCreatedLabels()
    {
        foreach (string key in _createdLabelKeys)
        {
            _client.DeleteLabel(key);
        }

        _createdLabelKeys.Clear();
    }

    private string CreateLabel(CalendarLabel label)
    {
        string key = _client.AddLabel(label).Data!.Data!;
        _createdLabelKeys.Add(key);
        return key;
    }

    [Test]
    [Category("Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    public void CreateLabel()
    {
        CalendarLabel label = CalendarLabelClient.CreateLabel(TestDataGenerator.UniqueName("Label"), StartDate, EndDate);

        RestResponse<ApiResponse<string>> response = _client.AddLabel(label);
        _createdLabelKeys.Add(response.Data!.Data!);
        CalendarLabel? savedLabel = _client.FindLabel(response.Data.Data!, StartDate, EndDate);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data.Success, Is.True);
            Assert.That(savedLabel, Is.Not.Null);
            Assert.That(savedLabel!.Name, Is.EqualTo(label.Name));
            Assert.That(savedLabel.BackColor, Is.EqualTo(label.BackColor));
            Assert.That(savedLabel.StartDate, Is.EqualTo(label.StartDate));
            Assert.That(savedLabel.EndDate, Is.EqualTo(label.EndDate));
        });
    }

    [Test]
    [Category("Regression")]
    public void UpdateLabel()
    {
        CalendarLabel label = CalendarLabelClient.CreateLabel(TestDataGenerator.UniqueName("Label"), StartDate, EndDate);
        label.Key = CreateLabel(label);
        label.Name = TestDataGenerator.UniqueName("Updated label");
        label.BackColor = "#00ff00";

        RestResponse<ApiResponse> response = _client.UpdateLabel(label);
        CalendarLabel? savedLabel = _client.FindLabel(label.Key, StartDate, EndDate);

        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Success, Is.True);
            Assert.That(savedLabel, Is.Not.Null);
            Assert.That(savedLabel!.Name, Is.EqualTo(label.Name));
            Assert.That(savedLabel.BackColor, Is.EqualTo("#00ff00"));
        });
    }

    [Test]
    [Category("Regression")]
    public void DeleteLabel()
    {
        string key = CreateLabel(CalendarLabelClient.CreateLabel(TestDataGenerator.UniqueName("Label"), StartDate, EndDate));

        RestResponse<ApiResponse> response = _client.DeleteLabel(key);

        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Success, Is.True);
            Assert.That(_client.FindLabel(key, StartDate, EndDate), Is.Null);
        });
    }

    [Test]
    [Category("Regression")]
    public void CreateLabelWithEmptyNameReturnsError()
    {
        RestResponse<ApiResponse<string>> response = _client.AddLabel(CalendarLabelClient.CreateLabel(string.Empty, StartDate, EndDate));

        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Success, Is.False);
            Assert.That(response.Data.ErrorDescription, Is.EqualTo("Name cannot be empty"));
            Assert.That(response.Data.Data, Is.Null);
        });
    }
}
