using API.Model.FacebookAds;
using API.Model.QuickBook;
using API.Model.Twilio;
using System.Net;

namespace API.Shared
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T ResponseData { get; set; }
        public ApiErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorTitle { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public TwilioAPIError TwilioAPIError { get; set; }
        public FacebookAdsError FacebookAdsError { get; set; }
        public ErrorMessageInfo[] errorMessageInfos { get; set; }
        public QuickBookError QuickBookError { get; set; }
        public string Message { get; set; }
    }

    public class ErrorMessageInfo 
    { 
        public string Key { get; set; }

        public string Value { get; set; }

        public string Message { get; set; }
    }
}
