namespace API.Model.Twilio
{
    public class TwilioSMS
    {
        public string From { get; set; }
        public string AuthToken { get; set; }
        public string AccountSID { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
    public class TwilioWhatsapp
    {
        public string From { get; set; }
        public string AuthToken { get; set; }
        public string AccountSID { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string? MediaUrl { get; set; }
    }
}
