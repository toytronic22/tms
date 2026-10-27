using System.Net;
using Reqres.Core.Clients;
using Reqres.Core.Models.Requests;
using Reqres.Core.Models.Responses;
using RestSharp;

namespace Reqres.Tests;

public class AuthTests : BaseApiTest
{
    private AuthClient _client = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _client = new AuthClient(BaseUrl);
    }

    [Test]
    public void RegisterWithValidDataReturnsToken()
    {
        AuthRequest request = new AuthRequest { Email = "eve.holt@reqres.in", Password = "pistol" };

        RestResponse<RegisterResponse> response = _client.Register<RegisterResponse>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Id, Is.EqualTo(4));
            Assert.That(response.Data.Token, Is.Not.Empty);
        });
    }

    [Test]
    public void RegisterWithoutPasswordReturnsError()
    {
        AuthRequest request = new AuthRequest { Email = "sydney@fife" };

        RestResponse<ErrorResponse> response = _client.Register<ErrorResponse>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.Data!.Error, Is.EqualTo("Missing password"));
    }

    [Test]
    public void RegisterUndefinedUserReturnsError()
    {
        AuthRequest request = new AuthRequest { Email = "unknown@user.com", Password = "qwerty" };

        RestResponse<ErrorResponse> response = _client.Register<ErrorResponse>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.Data!.Error, Is.EqualTo("Note: Only defined users succeed registration"));
    }

    [Test]
    public void LoginWithValidDataReturnsToken()
    {
        AuthRequest request = new AuthRequest { Email = "eve.holt@reqres.in", Password = "cityslicka" };

        RestResponse<LoginResponse> response = _client.Login<LoginResponse>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data!.Token, Is.Not.Empty);
    }

    [Test]
    public void LoginWithoutPasswordReturnsError()
    {
        AuthRequest request = new AuthRequest { Email = "peter@klaven" };

        RestResponse<ErrorResponse> response = _client.Login<ErrorResponse>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.Data!.Error, Is.EqualTo("Missing password"));
    }
}
