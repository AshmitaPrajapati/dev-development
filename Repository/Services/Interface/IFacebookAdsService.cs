using API.DataTransferObjects.FacebookAdsDto;
using API.Model.FacebookAds;
using API.Shared;

namespace API.Services.Interface
{
    public interface IFacebookAdsService
    {
        Task<ApiResponse<AddInfoListResponseDto>> GetAllAds(string? ManagerId);

        Task<ApiResponse<AdInfoResponse>> GetAdInfo(string AdId);

        Task<ApiResponse<AdInfoResponse>> CreateAd(CreateAdDto CreateAdDto);

        Task<ApiResponse<AdSetInfoListResponse>> GetAllAdSets();

        Task<ApiResponse<PageInfoListResponse>> GetAllPages();
    }
}
