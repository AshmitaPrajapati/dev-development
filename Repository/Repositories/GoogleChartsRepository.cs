using API.DataTransferObjects.GoogleChartsDtos;
using API.Services.Repositories.Interfaces;
using API.Shared;
using System.Net;

namespace API.Services.Repositories
{
    public class GoogleChartsRepository : IGoogleChartsRepository
    {
        public async Task<ApiResponse<List<ProductDto>>> GetAllProductData() 
        {
            var products = new List<ProductDto>
            {
                    new ProductDto { Id = "p01", Name = "Product 1", Price = 100, Quantity = 20 },
                    new ProductDto { Id = "p02", Name = "Product 2", Price = 120, Quantity = 12 },
                    new ProductDto { Id = "p03", Name = "Product 3", Price = 80, Quantity = 60 },
                    new ProductDto { Id = "p04", Name = "Product 4", Price = 290, Quantity = 34 },
                    new ProductDto { Id = "p05", Name = "Product 5", Price = 200, Quantity = 29 }
            };

            return new ApiResponse<List<ProductDto>> { IsSuccess = true, ResponseData = products, StatusCode = HttpStatusCode.OK };
        }
    }
}
