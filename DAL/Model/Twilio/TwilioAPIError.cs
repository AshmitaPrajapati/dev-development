using System.Runtime.Serialization;

namespace API.Model.Twilio
{
    [DataContract]
    public class TwilioAPIError
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "status")]
        public int Status { get; set; }
    }
}
