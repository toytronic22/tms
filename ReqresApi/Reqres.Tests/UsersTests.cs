using System.Net;
using Reqres.Core.Clients;
using Reqres.Core.Models;
using Reqres.Core.Models.Requests;
using Reqres.Core.Models.Responses;
using RestSharp;

namespace Reqres.Tests;

public class UsersTests : BaseApiTest
{
    private UsersClient _client = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _client = new UsersClient(BaseUrl);
    }

    [Test]
    public void GetUsersFirstPageReturnsSixUsers()
    {
        RestResponse<PageResponse<User>> response = _client.GetUsers(1);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Page, Is.EqualTo(1));
            Assert.That(response.Data.PerPage, Is.EqualTo(6));
            Assert.That(response.Data.Data, Has.Count.EqualTo(6));
            Assert.That(response.Data.Data[0].FirstName, Is.EqualTo("George"));
        });
    }

    [Test]
    public void GetUsersSecondPageStartsWithMichael()
    {
        RestResponse<PageResponse<User>> response = _client.GetUsers(2);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Page, Is.EqualTo(2));
            Assert.That(response.Data.TotalPages, Is.EqualTo(2));
            Assert.That(response.Data.Data[0].Id, Is.EqualTo(7));
            Assert.That(response.Data.Data[0].FirstName, Is.EqualTo("Michael"));
        });
    }

    [Test]
    public void GetSingleUserReturnsJanetWeaver()
    {
        RestResponse<DataResponse<User>> response = _client.GetUser(2);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Data.Id, Is.EqualTo(2));
            Assert.That(response.Data.Data.FirstName, Is.EqualTo("Janet"));
            Assert.That(response.Data.Data.LastName, Is.EqualTo("Weaver"));
            Assert.That(response.Data.Data.Email, Is.EqualTo("janet.weaver@reqres.in"));
        });
    }

    [Test]
    public void GetNonExistentUserReturnsNotFound()
    {
        RestResponse<DataResponse<User>> response = _client.GetUser(23);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public void CreateUserReturnsCreatedUser()
    {
        UserRequest user = new UserRequest { Name = "morpheus", Job = "leader" };

        RestResponse<CreateUserResponse> response = _client.CreateUser(user);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Name, Is.EqualTo(user.Name));
            Assert.That(response.Data.Job, Is.EqualTo(user.Job));
            Assert.That(response.Data.Id, Is.Not.Empty);
            Assert.That(response.Data.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(5).Minutes);
        });
    }

    [Test]
    public void UpdateUserWithPutChangesJob()
    {
        UserRequest user = new UserRequest { Name = "morpheus", Job = "zion resident" };

        RestResponse<UpdateUserResponse> response = _client.UpdateUser(2, user);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Name, Is.EqualTo(user.Name));
            Assert.That(response.Data.Job, Is.EqualTo(user.Job));
            Assert.That(response.Data.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(5).Minutes);
        });
    }

    [Test]
    public void UpdateUserWithPatchChangesJob()
    {
        UserRequest user = new UserRequest { Name = "morpheus", Job = "captain" };

        RestResponse<UpdateUserResponse> response = _client.PatchUser(2, user);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data!.Job, Is.EqualTo(user.Job));
    }

    [Test]
    public void DeleteUserReturnsNoContent()
    {
        RestResponse response = _client.DeleteUser(2);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(response.Content, Is.Empty);
        });
    }
}
