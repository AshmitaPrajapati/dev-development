namespace API.DataTransferObjects.ConfigurationDtos
{
    public class ConfigurationDto
    {
        public string Key { get; set; }

        public string Configuration { get; set; }

        public int SubMenuItemId { get; set; }

        public dynamic[] Result { get; set; }

        public dynamic[] GoogleAuthData { get; set; }

        public dynamic[] GoogleSubSheetData { get; set; }

        public dynamic[] QuickBookApplicationCredencialData { get; set; }
    }
}
