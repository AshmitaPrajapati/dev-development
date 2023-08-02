using API.DataTransferObjects.GoogleSheetDtos;
using API.Shared;

namespace API.Services.Interface
{
    public interface IGoogleSheetService
    {
        Task<ApiResponse<List<Dictionary<string, string>>>> ReadSheet(string sheetName, string sheetId, string subSheetName);

        Task<ApiResponse<string>> AddSheet(AddEditProductModel oProductDto, string sheetName, string sheetId);

        Task<ApiResponse<ProductResponseDto>> Edit(int Index, string sheetName, string sheetId, string subSheetName);

        Task<ApiResponse<string>> UpdateRow(AddEditProductModel oProductDto, int Index, string sheetName, string sheetId, string subSheetName);

        Task<ApiResponse<bool>> DeleteRow(int Index, string sheetName, string sheetId);
    }
}
