using System.Runtime.Serialization;

namespace API.DataTransferObjects.FacebookAdsDto
{
    [DataContract]
    public class AddInfoListResponseDto
    {
        [DataMember(Name = "data")]
        public List<AdInfoResponse> AdInfoList { get; set; }

        [DataMember(Name = "paging")]
        public Paging? Paging { get; set; }
    }

    [DataContract]
    public class AdInfoResponse
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "created_time")]
        public string CreatedTime { get; set; }
        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }
        [DataMember(Name = "preview_shareable_link")]
        public string PreviewLink { get; set; }

        [DataMember(Name = "creative")]
        public CreativeResponse Creative { get; set; }
    }

    [DataContract]
    public class CreativeResponse
    {
        [DataMember(Name = "id")]
        public string AdCreativeId { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "image_url")]
        public string ImageURL { get; set; }

        [DataMember(Name = "link_url")]
        public string LinkURL { get; set; }
    }
    public class Paging
    {
        public Cursors? cursors { get; set; }
        public string next { get; set; }
    }
    public class Cursors
    {
        public string before { get; set; }
        public string? after { get; set; }
    }

    [DataContract]
    public class AdSetInfoListResponse
    {
        [DataMember(Name = "data")]
        public List<AdSetInfoResponse> AdSetInfoList { get; set; }

        [DataMember(Name = "paging")]
        public Paging? Paging { get; set; }
    }

    [DataContract]
    public class AdSetInfoResponse
    {
        [DataMember(Name = "id")]
        public string AdSetID { get; set; }

        [DataMember(Name = "name")]
        public string AdSetName { get; set; }

    }

    [DataContract]
    public class PageInfoListResponse
    {
        [DataMember(Name = "data")]
        public List<PageInfoResponse> PageInfoList { get; set; }

        [DataMember(Name = "paging")]
        public Paging Paging { get; set; }
    }

    [DataContract]
    public class PageInfoResponse
    {
        [DataMember(Name = "id")]
        public string PageID { get; set; }

        [DataMember(Name = "name")]
        public string PageName { get; set; }
    }
}
