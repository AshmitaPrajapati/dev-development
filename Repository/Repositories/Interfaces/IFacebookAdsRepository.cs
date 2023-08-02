using API.DataTransferObjects.FacebookAdsDto;
using API.Model.FacebookAds;
using API.Shared;

namespace API.Services.Repositories.Interfaces
{
    public interface IFacebookAdsRepository
    {
        Task<ApiResponse<AddInfoListResponseDto>> GetAllAds(string AdsManagerId, string nextToken);

        Task<ApiResponse<AdInfoResponse>> GetAdInfo(string AdId);

        Task<ApiResponse<AdInfoResponse>> CreateAd(CreateAdDto CreateAdDto);

        Task<ApiResponse<AdSetInfoListResponse>> GetAllAdSets(string AdsManagerId, string nextToken);

        Task<ApiResponse<PageInfoListResponse>> GetAllPages(string nextToken);
    }
}
