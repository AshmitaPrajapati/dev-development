using API.Shared;
using DAL.Model.AzureCosmosMongoDB;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Repository.Repositories.Interfaces;
using System.Net;

namespace Repository.Repositories
{
    public class AzureCosmosMongoDBRepository : IAzureCosmosMongoDBRepository
    {
        private readonly IConfiguration _config;
        private MongoClient _client;
        private IMongoDatabase _dataBase;
        private IMongoCollection<StudentModel> _student;

        public AzureCosmosMongoDBRepository(IConfiguration config)
        {
            _config = config;
            _client = new MongoClient(_config.GetSection("MongoDbSetting:MongoDbPrimaryConnection").Value);
            _dataBase = _client.GetDatabase("advanture");
            _student = _dataBase.GetCollection<StudentModel>("NewStudent");
        }

        public async Task<ApiResponse<List<StudentModel>>> GetAllStudents()
        {
            var students = new List<StudentModel>();
            try
            {
                students = await _student.Find(Builders<StudentModel>.Filter.Empty).ToListAsync();

                return new ApiResponse<List<StudentModel>>()
                {
                    IsSuccess = true,
                    ResponseData = students
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<StudentModel>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApiResponse<string>> AddStudent(StudentModel model)
        {
            try
            {
                _student.InsertOne(model);
                return new ApiResponse<string>()
                {
                    IsSuccess = true,
                    Message = "Student Add Successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApiResponse<string>> UpdateStudent(StudentModel model)
        {
            try
            {
                var filter = Builders<StudentModel>.Filter.Eq(s => s.Id, model.Id);
                var update = Builders<StudentModel>.Update.Set(s => s.Name, model.Name);
                _student.UpdateOne(filter, update);

                return new ApiResponse<string>()
                {
                    IsSuccess = true,
                    Message = "Update Successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteStudent(int studentId)
        {
            try
            {
                var deleteFilter = Builders<StudentModel>.Filter.Eq(s => s.Id, studentId);
                _student.DeleteOne(deleteFilter);
                return new ApiResponse<bool>()
                {
                    IsSuccess = true,
                    Message = "deleted success",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
