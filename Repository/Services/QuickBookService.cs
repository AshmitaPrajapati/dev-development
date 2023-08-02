using API.Database;
using API.Services.Repositories.Interfaces;
using API.Shared;
using DAL.DataTransferObjects.QuickBookDtos;
using DAL.Model.QuickBook;
using Newtonsoft.Json;
using Repository.Repositories.Interfaces;
using Repository.Services.Interface;

namespace Repository.Services
{
    public class QuickBookService : IQuickBookService
    {
        private readonly IQuickBookAuthRepository _quickBookAuthRepository;
        private readonly IQuickBookRepository _quickBookRepository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ApplicationDbContext _dbContext;

        public QuickBookService(IQuickBookAuthRepository quickBookAuthRepository, IQuickBookRepository quickBookRepository, IConfigurationRepository configurationRepository, ApplicationDbContext dbContext)
        {
            _quickBookAuthRepository = quickBookAuthRepository;
            _quickBookRepository = quickBookRepository;
            _configurationRepository = configurationRepository;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<ItemQueryResponseDto>> GetItemByQuery(string applicationName, string country)
        {
            var result = GetCredencial(applicationName, country);
            var accessTokenResult = GetAuthToken(result.ClientId, result.ClientScreat, result.RefreshToken);

            return await _quickBookRepository.GetItemByQuery(accessTokenResult, result.ClientId, result.ClientScreat, result.CompanyId);
        }

        public async Task<ApiResponse<ItemResponse>> CreateItem(string applicationName, string country, ItemAddUpdateModel model)
        {
            var result = GetCredencial(applicationName, country);
            var accessTokenResult = GetAuthToken(result.ClientId, result.ClientScreat, result.RefreshToken);

            return await _quickBookRepository.CreateItem(accessTokenResult, result.CompanyId, model);
        }

        public async Task<ApiResponse<ItemResponse>> UpdateItem(string applicationName, string country, ItemAddUpdateModel model)
        {
            var result = GetCredencial(applicationName, country);
            var accessTokenResult = GetAuthToken(result.ClientId, result.ClientScreat, result.RefreshToken);
            var oItem = await GetItemById(accessTokenResult, result.CompanyId, model.Id);

            if (oItem.IsSuccess)
            {
                model.SyncToken = oItem.ResponseData.Item.SyncToken;
            }

            return await _quickBookRepository.UpdateItem(accessTokenResult, result.CompanyId, model);
        }

        public async Task<ApiResponse<ItemResponse>> GetItemById(string accessTokenResult, string companyId, string Id)
        {
            return await _quickBookRepository.GetItemById(accessTokenResult, companyId, Id);
        }

        public string GetAuthToken(string clientId, string clientSecret, string refreshToken)
        {
            var result = _quickBookAuthRepository.Auth(clientId, clientSecret, refreshToken);

            if (result.Result.IsSuccess)
            {
                return result.Result.ResponseData.AccessToken;
            }
            else
            {
                return "";
            }
        }

        public QuickBookCredencialModel GetCredencial(string applicationName, string country)
        {
            var configQuickBook = _dbContext.DemoConfigEntities.ToList().LastOrDefault(x => x.SubMenuItemId == 9);
            var jsonData = "[" + configQuickBook.Configuration.Remove(configQuickBook.Configuration.Length - 1, 1) + "]";

            List<QuickBookCredencialModel> quicks = JsonConvert.DeserializeObject<List<QuickBookCredencialModel>>(jsonData);

            var result = quicks.FirstOrDefault(x => x.CountryName == country && x.Application == applicationName);

            return result;
        }
    }
}
