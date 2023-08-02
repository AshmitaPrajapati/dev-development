using API.Database;
using API.DataTransferObjects.FacebookAdsDto;
using API.Model.FacebookAds;
using API.Services.Interface;
using API.Services.Repositories.Interfaces;
using API.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace API.Services
{
    public class FacebookAdsService : IFacebookAdsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFacebookAdsRepository _facebookAdsRepository;

        public FacebookAdsService(IFacebookAdsRepository facebookAdsRepository, ApplicationDbContext dbContext)
        {
            _facebookAdsRepository = facebookAdsRepository;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<AddInfoListResponseDto>> GetAllAds(string? ManagerId)
        {
            var next = false;
            var AdInfoListResult = new AddInfoListResponseDto();
            AdInfoListResult.AdInfoList = new List<AdInfoResponse>();
            ErrorMessageInfo[] errorMessageInfo = new ErrorMessageInfo[] { };
            int index = Array.IndexOf(errorMessageInfo, null);

            var allManagersValue = await _dbContext.DemoConfigEntities.Where(x => x.SubMenuItemId == 10).FirstOrDefaultAsync();

            var managerIds = allManagersValue.Configuration;

            var config = "[" + managerIds.Remove(managerIds.Length - 1, 1) + "]";

            var configData = JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, object>>>(config);

            var finalValue = configData.Select(d => d.Values.ToArray()).ToArray();

            if (ManagerId != "" && ManagerId != null && !string.IsNullOrEmpty(ManagerId))
            {
                Array.Resize(ref errorMessageInfo, errorMessageInfo.Length + 1);
                index++;

                string nextToken = string.Empty;

                var result = await _facebookAdsRepository.GetAllAds("act_" + ManagerId, nextToken);
                if (result.ResponseData != null)
                {
                    AdInfoListResult.AdInfoList.AddRange(result.ResponseData.AdInfoList);
                }

                var errorInfo = new ErrorMessageInfo()
                {
                    Key = "ManagerID",
                    Value = ManagerId,
                    Message = result?.FacebookAdsError?.FBError?.Message
                };

                if (index != -1)
                {
                    errorMessageInfo[index] = errorInfo;
                }
            }
            else 
            {
                foreach (var item in finalValue)
                {
                    Array.Resize(ref errorMessageInfo, errorMessageInfo.Length + 1);
                    index++;

                    string nextToken = string.Empty;

                    var result = await _facebookAdsRepository.GetAllAds("act_" + (string)item[1], nextToken);
                    if (result.ResponseData != null)
                    {
                        AdInfoListResult.AdInfoList.AddRange(result.ResponseData.AdInfoList);
                    }

                    var errorInfo = new ErrorMessageInfo()
                    {
                        Key = (string)item[0],
                        Value = (string)item[1],
                        Message = result?.FacebookAdsError?.FBError?.Message
                    };

                    if (index != -1)
                    {
                        errorMessageInfo[index] = errorInfo;
                    }
                }
            }

            return new ApiResponse<AddInfoListResponseDto>
            {
                IsSuccess = true,
                ResponseData = AdInfoListResult,
                errorMessageInfos = errorMessageInfo,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ApiResponse<AdInfoResponse>> GetAdInfo(string AdId)
        {
            var result = await _facebookAdsRepository.GetAdInfo(AdId);

            return result;
        }

        public async Task<ApiResponse<AdInfoResponse>> CreateAd(CreateAdDto CreateAdDto)
        {
            var result = await _facebookAdsRepository.CreateAd(CreateAdDto);

            return result;
        }

        public async Task<ApiResponse<AdSetInfoListResponse>> GetAllAdSets()
        {
            var next = false;
            var AdInfoSetListResult = new AdSetInfoListResponse();
            AdInfoSetListResult.AdSetInfoList = new List<AdSetInfoResponse>();

            var allManagersValue = await _dbContext.DemoConfigEntities.Where(x => x.SubMenuItemId == 10).FirstOrDefaultAsync();

            var managerIds = allManagersValue.Configuration;

            var config = "[" + managerIds.Remove(managerIds.Length - 1, 1) + "]";

            var configData = JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, object>>>(config);

            var finalValue = configData.Select(d => d.Values.ToArray()).ToArray();

            foreach (var item in finalValue)
            {
                string nextToken = string.Empty;

                do
                {
                    var result = await _facebookAdsRepository.GetAllAdSets("act_" + (string)item[1], nextToken);
                    if (result.ResponseData != null)
                    {
                        next = result.ResponseData.AdSetInfoList.Count() > 0;
                    }
                    else
                    {
                        next = false;
                    }

                    if (next)
                    {
                        AdInfoSetListResult.AdSetInfoList.AddRange(result.ResponseData.AdSetInfoList);
                        nextToken = result.ResponseData.Paging.cursors.after;
                    }

                } while (next);

            }

            return new ApiResponse<AdSetInfoListResponse>
            {
                IsSuccess = true,
                ResponseData = AdInfoSetListResult,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ApiResponse<PageInfoListResponse>> GetAllPages()
        {
            var next = false;
            var AdInfoListResult = new PageInfoListResponse();
            AdInfoListResult.PageInfoList = new List<PageInfoResponse>();

            var allManagersValue = await _dbContext.DemoConfigEntities.Where(x => x.SubMenuItemId == 10).FirstOrDefaultAsync();

            var managerIds = allManagersValue.Configuration;

            var config = "[" + managerIds.Remove(managerIds.Length - 1, 1) + "]";

            var configData = JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, object>>>(config);

            var finalValue = configData.Select(d => d.Values.ToArray()).ToArray();

            foreach (var item in finalValue)
            {
                string nextToken = string.Empty;

                do
                {
                    var result = await _facebookAdsRepository.GetAllPages(nextToken);
                    if (result.ResponseData != null)
                    {
                        next = result.ResponseData.PageInfoList.Count() > 0;
                    }
                    else
                    {
                        next = false;
                    }

                    if (next)
                    {
                        AdInfoListResult.PageInfoList.AddRange(result.ResponseData.PageInfoList);
                        nextToken = result.ResponseData.Paging.cursors.after;
                    }

                } while (next);

            }

            return new ApiResponse<PageInfoListResponse>
            {
                IsSuccess = true,
                ResponseData = AdInfoListResult,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
