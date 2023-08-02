using API.DataTransferObjects.GoogleChartsDtos;
using API.Services.Interface;
using API.Services.Repositories.Interfaces;
using API.Shared;

namespace API.Services
{
    public class GoogleChartsService: IGoogleChartsService
    {
        private readonly IGoogleChartsRepository _googleChartsRepository;

        public GoogleChartsService(IGoogleChartsRepository googleChartsRepository) 
        {
            _googleChartsRepository = googleChartsRepository;
        }

        public async Task<ApiResponse<List<ProductDto>>> GetAllProductData()
        {
            return await _googleChartsRepository.GetAllProductData();
        }
    }
}
