using System.Runtime.Serialization;

namespace API.Model.QuickBook
{
    [DataContract]
    public class QuickBookError
    {
        [DataMember(Name = "error")]
        public string Message { get; set; }
        [DataMember(Name = "error_description")]
        public string Description { get; set; }
        [DataMember(Name = "Fault")]
        public Fault Fault { get; set; }
    }

    [DataContract]
    public class Error12
    {
        [DataMember(Name = "Message")]
        public string Message { get; set; }
        [DataMember(Name = "Detail")]
        public string Detail { get; set; }
    }

    [DataContract]
    public class Fault
    {
        [DataMember(Name = "Error")]
        public List<Error12> Error { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
