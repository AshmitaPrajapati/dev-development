using System.ComponentModel.DataAnnotations;

namespace API.Model.FacebookAds
{
    public class Creative
    {
        public string title { get; set; }
        public string body { get; set; }
        public string image_url { get; set; }
        public string link_url { get; set; }
    }
    public class CreativeForAds
    {
        public string creative_id { get; set; }
    }
    public class Ads
    {
        public CreativeForAds creative { get; set; }
        public string name { get; set; }
        public string adset_id { get; set; }
        public string status { get; set; }
    }

    public class CallToAction
    {
        [Required]
        public string? type { get; set; }
    }

    public class LinkData
    {
        [Required]
        public string? link { get; set; }
        public string? caption { get; set; }
        public string? message { get; set; }
        public string? name { get; set; }
        public string? attachment_style { get; set; }
        public string? image_hash { get; set; }
        public CallToAction call_to_action { get; set; }
    }

    public class ObjectStorySpec
    {
        [Required]
        public string page_id { get; set; }
        public LinkData link_data { get; set; }
    }

    public class CreateAdCreativeDto
    {
        [Required]
        public string body { get; set; }
        public string? image_url { get; set; }
        public string? LinkUrl { get; set; }
        public ObjectStorySpec object_story_spec { get; set; }
    }

    public class CreateAdDto
    {
        public string AdsManagerId { get; set; }
        [Required]
        public string AdSetId { get; set; }
        [Required]
        public string AdName { get; set; }
        public CreateAdCreativeDto CreateAdCreativeDto { get; set; }

    }
}
