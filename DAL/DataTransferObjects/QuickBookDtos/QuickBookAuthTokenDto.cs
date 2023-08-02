using System.Runtime.Serialization;

namespace API.DataTransferObjects.QuickBookDtos
{
    [DataContract]
    public class QuickBookAuthTokenDto
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}
