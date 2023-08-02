using System.Runtime.Serialization;

namespace API.DataTransferObjects.TwilioDtos
{
    [DataContract]
    public class ResponseVMDto
    {
        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "from")]
        public string From { get; set; }

        [DataMember(Name = "date_updated")]
        public string UpdatedOn { get; set; }

        [DataMember(Name = "price")]
        public object price { get; set; }

        [DataMember(Name = "error_message")]
        public object ErrorMessage { get; set; }

        [DataMember(Name = "uri")]
        public string URI { get; set; }

        [DataMember(Name = "account_sid")]
        public string AccountSID { get; set; }

        [DataMember(Name = "num_media")]
        public string NoOfMedia { get; set; }

        [DataMember(Name = "to")]
        public string To { get; set; }

        [DataMember(Name = "Name")]
        public string CreatedOn { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "sid")]
        public string SID { get; set; }

        [DataMember(Name = "error_code")]
        public object ErrorCode { get; set; }

        [DataMember(Name = "price_unit")]
        public string PriceUnit { get; set; }

        [DataMember(Name = "api_version")]
        public string APIVersion { get; set; }
    }
}
