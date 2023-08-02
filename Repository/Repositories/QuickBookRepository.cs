using API.Shared;
using DAL.DataTransferObjects.QuickBookDtos;
using DAL.Model.QuickBook;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Repository.Repositories.Base.QuickBook;
using Repository.Repositories.Interfaces;
using RestSharp;

namespace Repository.Repositories
{
    public class QuickBookRepository : QuickBookBaseRepository, IQuickBookRepository
    {
        public QuickBookRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<ApiResponse<ItemQueryResponseDto>> GetItemByQuery(string accessToken, string clientId, string clientSecret, string companyId)
        {
           var query = new List<Parameter>()
           {
               new Parameter("minorversion","65", ParameterType.QueryString),
               new Parameter("query","select * from Item", ParameterType.QueryString)
           };

           return await SendAuthRequest<ItemQueryResponseDto>("company/" + companyId + "/query", accessToken, query, clientId, clientSecret, false, Method.GET);
        }

        public async Task<ApiResponse<ItemResponse>> CreateItem(string accessToken, string companyId, ItemAddUpdateModel model)
        {
            var query = new List<Parameter>()
            {
                new Parameter("minorversion","65", ParameterType.QueryString),
                new Parameter("Content-Type", "application/json",ParameterType.HttpHeader),
                new Parameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody)
            };

            return await SendAuthRequest<ItemResponse>("company/" + companyId + "/item", accessToken, query, null, null, false, Method.POST);
        }

        public async Task<ApiResponse<ItemResponse>> UpdateItem(string accessToken, string companyId, ItemAddUpdateModel model)
        {
            var query = new List<Parameter>()
            {
                new Parameter("minorversion","65", ParameterType.QueryString),
                new Parameter("Content-Type", "application/json",ParameterType.HttpHeader),
                new Parameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody)
            };

            return await SendAuthRequest<ItemResponse>("company/" + companyId + "/item", accessToken, query, null, null, false, Method.POST);
        }

        public async Task<ApiResponse<ItemResponse>> GetItemById(string accessToken, string companyId, string Id)
        {
            var query = new List<Parameter>()
            {
                new Parameter("minorversion","65", ParameterType.QueryString),
                new Parameter("Content-Type", "application/x-www-form-urlencoded",ParameterType.HttpHeader)
            };

            return await SendAuthRequest<ItemResponse>("company/" + companyId + "/item/" + Id, accessToken, query, null, null, false, Method.GET);
        }
    }
}
