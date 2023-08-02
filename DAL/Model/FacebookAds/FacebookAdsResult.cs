using System.Net;

namespace API.Model.FacebookAds
{
    public class FacebookAdsResult<T>
    {
        public T Result { get; set; }

        public FacebookAdsError Error { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
