using API.Helper;
using API.Model.FacebookAds;
using API.Shared;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace API.Services.Repositories.Base.FacebookAds
{
    public class FacebookAdsBaseRepository : IFacebookAdsBaseRepository
    {
        private IOptions<AppSettings> _appSettings;

        public FacebookAdsBaseRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected async Task<ApiResponse<TResult>> SendRequest<TResult>(string resource, List<Parameter> queries, Method method = Method.POST)
        {
            try
            {
                var client = CreateClient();
                IRestRequest request = new RestRequest(resource, method);

                request.Parameters.AddRange(queries);

                request.AddQueryParameter("access_token", _appSettings.Value.Accesstoken);

                var result = await client.ExecuteAsync<TResult>(request);

                if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Accepted)
                {
                    result.Data = JsonConvert.DeserializeObject<TResult>(result.Content);
                }

                return FormResult(result, client);
            }
            catch (Exception e)
            {
                return new ApiResponse<TResult> { IsSuccess = false };
            }
        }

        private ApiResponse<TResult> FormResult<TResult>(IRestResponse<TResult> result, IRestClient client)
        {
            if (result.IsSuccessful || result.StatusCode == HttpStatusCode.Conflict)
            {
                return new ApiResponse<TResult>
                {
                    ResponseData = result.Data,
                    IsSuccess = true,
                    StatusCode = result.StatusCode,
                };
            }

            var errors = JsonConvert.DeserializeObject<FacebookAdsError>(result.Content);
            return new ApiResponse<TResult>
            {
                IsSuccess = false,
                StatusCode = result.StatusCode,
                FacebookAdsError = errors
            };
        }

        private IRestClient CreateClient()
        {
            var client = new RestClient(_appSettings.Value.RootLink);
            client.AddHandler("application/json", () => new RestSharp.Serialization.Json.JsonSerializer());
            client.UseJson();

            return client;
        }
    }
}
