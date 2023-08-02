using API.DataTransferObjects.GoogleSheetDtos;
using API.Services.Repositories.Interfaces;
using API.Shared;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using static API.Shared.ApiFunctions;

namespace API.Services.Repositories
{
    public class GoogleSheetRepository : IGoogleSheetRepository
    {
        public static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        public static SheetsService service;

        public void Init(string sheetName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string relativePathToFile = Path.Combine(currentDirectory, "File", "app_client_secret.json");

            GoogleCredential credential;
            using (var stream = new FileStream(relativePathToFile, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }

            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = sheetName,
            });
        }

        public async Task<ApiResponse<List<Dictionary<string, string>>>> ReadSheet(string sheetName, string sheetId, string subSheetName)
        {
            try
            {
                Init(sheetName);

                var range = $"{subSheetName}!A:Z";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(sheetId, range);

                var response = request.Execute();

                IList<IList<object>> SheetValues = response.Values;

                List<string> fieldLists = new List<string>();

                List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

                if (SheetValues[0].Count() > 0)
                {
                    for (int j = 0; j < SheetValues[0].Count(); j++)
                    {
                        fieldLists.Add(SheetValues[0][j].ToString());
                    }
                }

                for (int i = 1; i < SheetValues.Count(); i++)
                {
                    var formattedItem = new Dictionary<string, string>();
                    var j = 0;

                    foreach (var field in fieldLists)
                    {
                        formattedItem.Add(field, SheetValues[i][j].ToString());
                        j++;
                    }

                    j = 0;
                    result.Add(formattedItem);
                }

                return ApiSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Dictionary<string, string>>>()
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApiResponse<string>> AddSheet(AddEditProductModel oProductDto, string sheetName, string sheetId)
        {
            try
            {
                Init(sheetName);
                var productData = string.Empty;

                if (oProductDto.Product != null)
                {
                    productData = string.Join(",", oProductDto.Product, oProductDto.Price);
                }
                else
                {
                    productData = string.Join(",", oProductDto.Username, oProductDto.Age, oProductDto.Gender);
                }

                InsertDimensionRequest insertRow = new InsertDimensionRequest();
                insertRow.Range = new DimensionRange()
                {
                    SheetId = 0,
                    Dimension = "ROWS",
                    StartIndex = 1,
                    EndIndex = 2
                };

                PasteDataRequest data = new PasteDataRequest
                {
                    Data = productData,
                    Delimiter = ",",
                    Coordinate = new GridCoordinate
                    {
                        ColumnIndex = 0,
                        RowIndex = 1,
                        SheetId = 0
                    },
                };

                BatchUpdateSpreadsheetRequest r = new BatchUpdateSpreadsheetRequest()
                {
                    Requests = new List<Request>
                {
                    new Request{ InsertDimension = insertRow },
                    new Request{ PasteData = data }
                }
                };

                BatchUpdateSpreadsheetResponse response1 = service.Spreadsheets.BatchUpdate(r, sheetId).Execute();

                return ApiSuccessResponse("Data Added Succesfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>()
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApiResponse<ProductResponseDto>> Edit(int Index, string sheetName, string sheetId, string subSheetName)
        {
            try
            {
                Init(sheetName);
                Index++;
                // Specifying Column Range for reading...
                var range = $"{subSheetName}!A" + Index.ToString() + ":B" + Index.ToString();
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(sheetId, range);
                // Ecexuting Read Operation...
                var response = request.Execute();
                // Getting all records from Column A to B...
                IList<IList<object>> SheetValues = response.Values;

                ProductResponseDto oProductDto = new ProductResponseDto() { Name = SheetValues.Select(o => o[0].ToString()).FirstOrDefault(), Price = SheetValues.Select(o => Convert.ToInt32(o[1])).FirstOrDefault() };

                return ApiSuccessResponse(oProductDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductResponseDto>()
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApiResponse<string>> UpdateRow(AddEditProductModel oProductDto, int Index, string sheetName, string sheetId, string subSheetName)
        {
            try
            {
                Init(sheetName);

                Index++;
                // Setting Cell Name...
                var range = $"{subSheetName}!A" + Index.ToString() + ":Z" + Index.ToString();
                var valueRange = new ValueRange();
                // Setting Cell Value...

                var oblist = new List<IList<object>>();

                if (oProductDto.Product != null)
                {
                    oblist.Add(new List<object>() { oProductDto.Product, oProductDto.Price });
                }
                else
                {
                    oblist.Add(new List<object>() { oProductDto.Username, oProductDto.Age, oProductDto.Gender });
                }

                valueRange.Values = oblist;

                // Performing Update Operation...
                var updateRequest = service.Spreadsheets.Values.Update(valueRange, sheetId, range);
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                var appendReponse = updateRequest.Execute();

                return ApiSuccessResponse("Data Update Successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>()
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteRow(int Index, string sheetName, string sheetId)
        {
            try
            {
                Init(sheetName);

                Index++;
                Request RequestBody = new Request()
                {
                    DeleteDimension = new DeleteDimensionRequest()
                    {
                        Range = new DimensionRange()
                        {
                            SheetId = 0,
                            Dimension = "ROWS",
                            StartIndex = Convert.ToInt32(Index - 1),
                            EndIndex = Convert.ToInt32(Index)
                        }
                    }
                };

                List<Request> RequestContainer = new List<Request>();
                RequestContainer.Add(RequestBody);

                BatchUpdateSpreadsheetRequest DeleteRequest = new BatchUpdateSpreadsheetRequest();
                DeleteRequest.Requests = RequestContainer;
                SpreadsheetsResource.BatchUpdateRequest Deletion = new SpreadsheetsResource.BatchUpdateRequest(service, DeleteRequest, sheetId);
                Deletion.Execute();

                return ApiSuccessResponse(true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>()
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
