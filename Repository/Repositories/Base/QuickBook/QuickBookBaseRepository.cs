using Microsoft.Extensions.Options;
using API.Model.QuickBook;
using API.Shared;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Repository.Repositories.Base.QuickBook
{
    public class QuickBookBaseRepository : IQuickBookBaseRepository
    {
        private readonly IConfiguration _configuration;

        public QuickBookBaseRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        protected async Task<ApiResponse<TResult>> SendAuthRequest<TResult>(string resource, string accessToken, List<Parameter> queries, string ClientId, string ClientSecret, bool IsForPDF = false, Method method = Method.POST)
        {
            try
            {
                var authLink = _configuration.GetSection("QBSettings:AuthLink").Value;
                var rootLink = _configuration.GetSection("QBSettings:RootLink").Value;

                var client = new RestClient((accessToken == null ? authLink : rootLink) + resource);
                client.Timeout = -1;

                var request = new RestRequest(method);
                request.Parameters.AddRange(queries);
                request.AddHeader("Authorization", accessToken == null ? GetAuthorizationHeader(ClientId + ":" + ClientSecret) : ("Bearer " + accessToken));

                if (IsForPDF)
                {
                    //request.AddHeader("Accept", "application/pdf");
                    //request.AddHeader("Content-Type", "application/pdf");
                    //var UrlArray = resource.Split('/');
                    //var uploadDirecotroy = "InvoicePDF/";
                    ////var uploadPath = Path.Combine(_env.WebRootPath, uploadDirecotroy);
                    ////if (!Directory.Exists(uploadPath))
                    ////    Directory.CreateDirectory(uploadPath);
                    ////var PDFPath = $"{uploadPath}Invoice_{UrlArray[UrlArray.Length - 2]}.pdf";
                    //IRestResponse response = await client.ExecuteAsync(request);
                    //if (response.IsSuccessful)
                    //{
                    //    //using (BinaryWriter writer = new BinaryWriter(File.Open(PDFPath, FileMode.Create))) { writer.Write(response.RawBytes); }
                    //    return new ApiResponse<TResult>
                    //    {
                    //        ResponseData = JsonConvert.DeserializeObject<TResult>("true"),
                    //        Message = $"Invoice_{UrlArray[UrlArray.Length - 2]}.pdf successfully downloaded.",
                    //        IsSuccess = true
                    //    };
                    //}
                    //return new ApiResponse<TResult>
                    //{
                    //    ResponseData = JsonConvert.DeserializeObject<TResult>("false"),
                    //    QuickBookError = JsonConvert.DeserializeObject<QuickBookError>(response.Content),
                    //    IsSuccess = false
                    //};
                }

                request.AddHeader("Accept", "application/json");
                IRestResponse<TResult> result = null;
                result = await client.ExecuteAsync<TResult>(request);

                return FormResult(result, client);
            }
            catch (Exception e)
            {
                return new ApiResponse<TResult> { IsSuccess = false, QuickBookError = new QuickBookError() { Message = e.Message } };
            }
        }

        private ApiResponse<TResult> FormResult<TResult>(IRestResponse<TResult> result, IRestClient client)
        {
            if (result.IsSuccessful && !result.Content.Contains("\"Fault\""))
            {
                return new ApiResponse<TResult>
                {
                    ResponseData = JsonConvert.DeserializeObject<TResult>(result.Content),
                    IsSuccess = true,
                    StatusCode = result.StatusCode,
                };
            }

            var errors = JsonConvert.DeserializeObject<QuickBookError>(result.Content);
            return new ApiResponse<TResult>
            {
                IsSuccess = false,
                StatusCode = result.StatusCode,
                QuickBookError = errors
            };
        }

        public string GetAuthorizationHeader(string credentials)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(credentials);
            return "Basic " + Convert.ToBase64String(bytes);
        }
    }
}
