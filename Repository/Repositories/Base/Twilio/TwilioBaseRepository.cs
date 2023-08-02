using API.Model.Twilio;
using API.Shared;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace API.Services.Repositories.Base.TwilioBaseRepository
{
    public class TwilioBaseRepository : ITwilioBaseRepository
    {
        private readonly IConfiguration _configuration;

        public TwilioBaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected async Task<ApiResponse<TResult>> SendRequest<TResult>(string resource, List<Parameter> queries, dynamic twilioSMS, Method method = Method.POST)
        {
            try
            {
                var client = CreateClient();
                IRestRequest request = new RestRequest(resource, method);

                request.Parameters.AddRange(queries);

                request.AddHeader("Authorization", GetAuthorizationHeader(twilioSMS.AccountSID + ":" + twilioSMS.AuthToken));
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

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

            var errors = JsonConvert.DeserializeObject<TwilioAPIError>(result.Content);
            return new ApiResponse<TResult>
            {
                IsSuccess = false,
                StatusCode = result.StatusCode,
                TwilioAPIError = errors
            };
        }

        public string GetAuthorizationHeader(string credentials)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(credentials);
            return "Basic " + Convert.ToBase64String(bytes);
        }

        private IRestClient CreateClient()
        {
            var client = new RestClient(_configuration.GetValue<string>("TASettings:RootLink"));
            client.AddHandler("application/json", () => new RestSharp.Serialization.Json.JsonSerializer());
            client.UseJson();

            return client;
        }
    }
}
