using Reqres.Core.Models;
using Reqres.Core.Models.Requests;
using Reqres.Core.Models.Responses;
using RestSharp;

namespace Reqres.Core.Clients;

public class UsersClient : ApiClient
{
    private const string UsersEndpoint = "api/users/";

    public UsersClient(string baseUrl) : base(baseUrl)
    {
    }

    public RestResponse<PageResponse<User>> GetUsers(int page)
    {
        Dictionary<string, string> queryParams = new Dictionary<string, string>
        {
            { "page", page.ToString() }
        };

        return Get<PageResponse<User>>(UsersEndpoint, queryParams);
    }

    public RestResponse<DataResponse<User>> GetUser(int id)
    {
        return Get<DataResponse<User>>(UsersEndpoint + id);
    }

    public RestResponse<CreateUserResponse> CreateUser(UserRequest user)
    {
        return Post<CreateUserResponse>(UsersEndpoint, user);
    }

    public RestResponse<UpdateUserResponse> UpdateUser(int id, UserRequest user)
    {
        return Put<UpdateUserResponse>(UsersEndpoint + id, user);
    }

    public RestResponse<UpdateUserResponse> PatchUser(int id, UserRequest user)
    {
        return Patch<UpdateUserResponse>(UsersEndpoint + id, user);
    }

    public RestResponse DeleteUser(int id)
    {
        return Delete(UsersEndpoint + id);
    }
}
