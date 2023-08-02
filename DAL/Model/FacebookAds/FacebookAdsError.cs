using System.Runtime.Serialization;

namespace API.Model.FacebookAds
{
    [DataContract]
    public class FacebookAdsError
    {
        [DataMember(Name = "Error")]
        public Error FBError { get; set; }
    }

    [DataContract]
    public class Error
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "type")]
        public string ErrorType { get; set; }
        [DataMember(Name = "code")]
        public int Code { get; set; }
        [DataMember(Name = "error_subcode")]
        public int ErrorSubCode { get; set; }
        [DataMember(Name = "is_transient")]
        public bool Is_Transient { get; set; }
        [DataMember(Name = "error_user_title")]
        public string ErrorTitle { get; set; }
        [DataMember(Name = "error_user_msg")]
        public string ErrorDescription { get; set; }
        [DataMember(Name = "fbtrace_id")]
        public string FBTraceId { get; set; }
    }
}
