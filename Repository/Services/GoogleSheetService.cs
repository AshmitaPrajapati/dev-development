using API.DataTransferObjects.GoogleSheetDtos;
using API.Services.Interface;
using API.Services.Repositories.Interfaces;
using API.Shared;

namespace API.Services
{
    public class GoogleSheetService : IGoogleSheetService
    {
        private readonly IGoogleSheetRepository _googleSheetRepository;

        public GoogleSheetService(IGoogleSheetRepository googleSheetRepository)
        {
            _googleSheetRepository = googleSheetRepository;
        }

        public async Task<ApiResponse<List<Dictionary<string, string>>>> ReadSheet(string sheetName, string sheetId, string subSheetName)
        {
            return await _googleSheetRepository.ReadSheet(sheetName, sheetId, subSheetName);
        }

        public async Task<ApiResponse<string>> AddSheet(AddEditProductModel oProductDto, string sheetName, string sheetId)
        {
            return await _googleSheetRepository.AddSheet(oProductDto, sheetName, sheetId);
        }

        public async Task<ApiResponse<ProductResponseDto>> Edit(int Index, string sheetName, string sheetId, string subSheetName)
        {
            return await _googleSheetRepository.Edit(Index, sheetName, sheetId, subSheetName);
        }

        public async Task<ApiResponse<string>> UpdateRow(AddEditProductModel oProductDto, int Index, string sheetName, string sheetId, string subSheetName)
        {
            return await _googleSheetRepository.UpdateRow(oProductDto, Index, sheetName, sheetId, subSheetName);
        }

        public async Task<ApiResponse<bool>> DeleteRow(int Index, string sheetName, string sheetId)
        {
            return await _googleSheetRepository.DeleteRow(Index, sheetName, sheetId);
        }
    }
}
