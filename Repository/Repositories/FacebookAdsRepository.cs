using API.DataTransferObjects.FacebookAdsDto;
using API.Helper;
using API.Model.FacebookAds;
using API.Services.Repositories.Base.FacebookAds;
using API.Services.Repositories.Interfaces;
using API.Shared;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace API.Services.Repositories
{
    public class FacebookAdsRepository : FacebookAdsBaseRepository, IFacebookAdsRepository
    {
        public FacebookAdsRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {}

        public async Task<ApiResponse<AddInfoListResponseDto>> GetAllAds(string AdsManagerId, string nextToken)
        {
            var query = new List<Parameter>()
            {
                new Parameter("fields", "name,status,created_time,updated_time,preview_shareable_link,creative{title,body,image_url,link_url}", ParameterType.QueryString),
                new Parameter("limit", "100", ParameterType.QueryString)
            };

            if (!string.IsNullOrEmpty(nextToken))
            {
                query.Add(new Parameter("after", nextToken, ParameterType.QueryString));
            }

            return await SendRequest<AddInfoListResponseDto>(AdsManagerId + "/ads", query, Method.GET);
        }

        public async Task<ApiResponse<AdInfoResponse>> GetAdInfo(string AdId)
        {
            return await SendRequest<AdInfoResponse>(AdId + "?fields=name,status,created_time,updated_time,preview_shareable_link,creative{title,body,image_url,link_url}", new List<Parameter>(), Method.GET);
        }

        public async Task<ApiResponse<AdInfoResponse>> CreateAd(CreateAdDto CreateAdDto) 
        {
            var quries = new List<Parameter>()
            {
                   new Parameter("application/json",JsonConvert.SerializeObject(CreateAdDto.CreateAdCreativeDto), ParameterType.RequestBody)
            };

            var response = await SendRequest<CreativeResponse>(CreateAdDto.AdsManagerId + "/adcreatives?fields=body,image_url,link_url,object_story_spec", quries, Method.POST);

            if (response.IsSuccess)
            {
                var oAds = new Ads() { adset_id = CreateAdDto.AdSetId, creative = new CreativeForAds() { creative_id = response.ResponseData.AdCreativeId }, name = CreateAdDto.AdName, status = "PAUSED" };

                quries = new List<Parameter>()
                {
                       new Parameter("application/json",JsonConvert.SerializeObject(oAds), ParameterType.RequestBody)
                };

                var AdsResponse = await SendRequest<AdInfoResponse>(CreateAdDto.AdsManagerId + "/ads?fields=creative,name", quries, Method.POST);

                if (AdsResponse.IsSuccess)
                {
                    return new ApiResponse<AdInfoResponse> { IsSuccess = true, ResponseData = AdsResponse.ResponseData, StatusCode = HttpStatusCode.OK };
                }
                return new ApiResponse<AdInfoResponse> { IsSuccess = false, ErrorMessage = response.FacebookAdsError.FBError.Message, StatusCode = HttpStatusCode.BadRequest };
            }
            else 
            {
                return new ApiResponse<AdInfoResponse> { IsSuccess = false, ErrorMessage = response.FacebookAdsError.FBError.Message, StatusCode = HttpStatusCode.BadRequest };
            }
        }

        public async Task<ApiResponse<AdSetInfoListResponse>> GetAllAdSets(string AdsManagerId, string nextToken)
        {
            var query = new List<Parameter>()
            {
                new Parameter("fields", "name", ParameterType.QueryString),
                new Parameter("limit", "500", ParameterType.QueryString)
            };

            if (!string.IsNullOrEmpty(nextToken))
            {
                query.Add(new Parameter("after", nextToken, ParameterType.QueryString));
            }

            return await SendRequest<AdSetInfoListResponse>(AdsManagerId + "/ads", query, Method.GET);
        }

        public async Task<ApiResponse<PageInfoListResponse>> GetAllPages(string nextToken)
        {
            var query = new List<Parameter>()
            {
                new Parameter("fields", "name", ParameterType.QueryString),
                new Parameter("limit", "500", ParameterType.QueryString)
            };

            if (!string.IsNullOrEmpty(nextToken))
            {
                query.Add(new Parameter("after", nextToken, ParameterType.QueryString));
            }

            return await SendRequest<PageInfoListResponse>("me/accounts", query, Method.GET);
        }
    }
}
