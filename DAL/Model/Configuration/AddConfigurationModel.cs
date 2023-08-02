namespace API.Model.Configuration
{
    public class AddConfigurationModel
    {
        public ConfigModel[]? keyValue { get; set; }

        public GoogleAuthModel? googleAuthModels { get; set; }

        public GoogleSubSheetModel? googleSubSheetModel { get; set; }

        public QuickBookModel? quickBookModel { get; set; }
    }

    public class ConfigModel
    {
        public string? Key { get; set; }

        public string? Value { get; set; }

        public string? From { get; set; }

        public string? ConatctNo { get; set; }

        public string? AuthToken { get; set; }

        public string? AccountSID { get; set; }

        public bool? IsSMS { get; set; }

        public bool? IsWhatsap { get; set; }

        public string? SheetId { get; set; }

        public string? SheetName { get; set; }

        public string? ClientId { get; set; }

        public string? ClientSecret { get; set; }

        public string? TenantId { get; set; }

        public string? Application { get; set; }
    }

    public class GoogleAuthModel
    {
        public KeyValuePair[]? keyValue { get; set; }
    }

    public class GoogleSubSheetModel
    {
        public SubSheetModel[] subSheetModels { get; set; }
    }

    public class QuickBookModel
    {
        public QuickBookConfigModel[]? quickBookConfigModels { get; set; }
    }

    public class SubSheetModel
    {
        public string? SheetName { get; set; }

        public string? SubSheetName { get; set; }

        public string? SubSheetUrl { get; set; }
    }

    public class QuickBookConfigModel
    {
        public string Application { get; set; }

        public string ClientId { get; set; }

        public string ClientScreat { get; set; }

        public string RefreshToken { get; set; }

        public string CompanyId { get; set; }

        public string CountryName { get; set; }
    }

    public class KeyValuePair
    {
        public string? Key { get; set; }

        public string? Value { get; set; }
    }
}

