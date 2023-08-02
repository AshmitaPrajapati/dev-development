namespace API.DataTransferObjects.ConfigurationDtos.TwilioConfigurationDto
{
    public class TwilioConfigurationDto
    {
        public string ConatctNo { get; set; }

        public string AuthToken { get; set; }

        public string AccountSID { get; set; }

        public bool isSMS { get; set; }

        public bool isWhatsap { get; set; }
    }
}
