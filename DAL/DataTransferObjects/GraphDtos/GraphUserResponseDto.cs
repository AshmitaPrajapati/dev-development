using System.Runtime.Serialization;

namespace API.DataTransferObjects.GraphDtos
{
    [DataContract]
    public class GraphUserResponseDto
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "preferredLanguage")]
        public string PreferredLanguage { get; set; }

        [DataMember(Name = "userPrincipalName")]
        public string UserPrincipalName { get; set; }
    }
}
