using System.Net;
using Reqres.Core.Clients;
using Reqres.Core.Models;
using Reqres.Core.Models.Responses;
using RestSharp;

namespace Reqres.Tests;

public class ResourcesTests : BaseApiTest
{
    private ResourcesClient _client = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _client = new ResourcesClient(BaseUrl);
    }

    [Test]
    public void GetResourcesReturnsFirstPage()
    {
        RestResponse<PageResponse<Resource>> response = _client.GetResources();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Page, Is.EqualTo(1));
            Assert.That(response.Data.Total, Is.EqualTo(12));
            Assert.That(response.Data.Data, Has.Count.EqualTo(6));
            Assert.That(response.Data.Data[0].Name, Is.EqualTo("cerulean"));
        });
    }

    [Test]
    public void GetSingleResourceReturnsFuchsiaRose()
    {
        RestResponse<DataResponse<Resource>> response = _client.GetResource(2);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Data.Name, Is.EqualTo("fuchsia rose"));
            Assert.That(response.Data.Data.Year, Is.EqualTo(2001));
            Assert.That(response.Data.Data.Color, Is.EqualTo("#C74375"));
            Assert.That(response.Data.Data.PantoneValue, Is.EqualTo("17-2031"));
        });
    }

    [Test]
    public void GetNonExistentResourceReturnsNotFound()
    {
        RestResponse<DataResponse<Resource>> response = _client.GetResource(23);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
