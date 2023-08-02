using API.DataTransferObjects.GoogleChartsDtos;
using API.Shared;

namespace API.Services.Repositories.Interfaces
{
    public interface IGoogleChartsRepository
    {
        Task<ApiResponse<List<ProductDto>>> GetAllProductData();
    }
}
