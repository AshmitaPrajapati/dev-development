using API.Shared;
using DAL.Model.AzureCosmosMongoDB;

namespace Repository.Services.Interface
{
    public interface IAzureCosmosMongoDBService
    {
        Task<ApiResponse<List<StudentModel>>> GetAllStudents();

        Task<ApiResponse<string>> AddStudent(StudentModel model);

        Task<ApiResponse<string>> UpdateStudent(StudentModel model);

        Task<ApiResponse<bool>> DeleteStudent(int studentId);
    }
}
