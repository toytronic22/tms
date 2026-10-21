using Reqres.Core.Models;
using Reqres.Core.Models.Responses;
using RestSharp;

namespace Reqres.Core.Clients;

public class ResourcesClient : ApiClient
{
    private const string ResourcesEndpoint = "api/unknown/";

    public ResourcesClient(string baseUrl) : base(baseUrl)
    {
    }

    public RestResponse<PageResponse<Resource>> GetResources()
    {
        return Get<PageResponse<Resource>>(ResourcesEndpoint);
    }

    public RestResponse<DataResponse<Resource>> GetResource(int id)
    {
        return Get<DataResponse<Resource>>(ResourcesEndpoint + id);
    }
}
