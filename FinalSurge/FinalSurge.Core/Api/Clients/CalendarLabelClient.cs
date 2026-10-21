using FinalSurge.Core.Api.Models;
using RestSharp;

namespace FinalSurge.Core.Api.Clients;

public class CalendarLabelClient : BaseApiClient
{
    private const string AddEndpoint = "CalendarLabelAdd";
    private const string UpdateEndpoint = "CalendarLabelUpdate";
    private const string ListEndpoint = "CalendarLabelList";
    private const string DeleteEndpoint = "CalendarLabelDelete";

    private readonly string _userKey;

    public CalendarLabelClient(string token, string userKey) : base(token)
    {
        _userKey = userKey;
    }

    public RestResponse<ApiResponse<string>> AddLabel(CalendarLabel label) =>
        Post<ApiResponse<string>>(AddEndpoint, label, new Dictionary<string, string>
        {
            ["scope"] = "USER",
            ["scopekey"] = _userKey,
            ["save_as_option"] = "false"
        });

    public RestResponse<ApiResponse> UpdateLabel(CalendarLabel label) =>
        Post<ApiResponse>(UpdateEndpoint, label, new Dictionary<string, string>
        {
            ["scope"] = "USER",
            ["scopekey"] = _userKey
        });

    public RestResponse<ApiResponse<List<CalendarLabel>>> GetLabels(DateTime startDate, DateTime endDate) =>
        Get<ApiResponse<List<CalendarLabel>>>(ListEndpoint, new Dictionary<string, string>
        {
            ["scope"] = "USER",
            ["scopekey"] = _userKey,
            ["startdate"] = FormatDate(startDate),
            ["enddate"] = FormatDate(endDate)
        });

    public RestResponse<ApiResponse> DeleteLabel(string labelKey) =>
        Get<ApiResponse>(DeleteEndpoint, new Dictionary<string, string>
        {
            ["cal_label_key"] = labelKey,
            ["scope"] = "USER",
            ["scopekey"] = _userKey
        });

    public static CalendarLabel CreateLabel(string name, DateTime startDate, DateTime endDate, string backColor = "#ff0000") => new()
    {
        Name = name,
        BackColor = backColor,
        ForeColor = "#ffffff",
        StartDate = FormatDateTime(startDate),
        EndDate = FormatDateTime(endDate)
    };

    public CalendarLabel? FindLabel(string labelKey, DateTime startDate, DateTime endDate) =>
        GetLabels(startDate, endDate).Data?.Data?.FirstOrDefault(label => label.Key == labelKey);
}
