using API.DataTransferObjects.GoogleChartsDtos;
using API.Shared;

namespace API.Services.Interface
{
    public interface IGoogleChartsService
    {
        Task<ApiResponse<List<ProductDto>>> GetAllProductData();
    }
}
