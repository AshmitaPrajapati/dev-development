using API.Shared;
using DAL.Model.AzureCosmosMongoDB;
using Repository.Repositories.Interfaces;
using Repository.Services.Interface;

namespace Repository.Services
{
    public class AzureCosmosMongoDBService : IAzureCosmosMongoDBService
    {
        private readonly IAzureCosmosMongoDBRepository _azureCosmosMongoDBRepository;

        public AzureCosmosMongoDBService(IAzureCosmosMongoDBRepository azureCosmosMongoDBRepository)
        {
            _azureCosmosMongoDBRepository = azureCosmosMongoDBRepository;
        }

        public async Task<ApiResponse<List<StudentModel>>> GetAllStudents()
        {
            return await _azureCosmosMongoDBRepository.GetAllStudents();
        }

        public async Task<ApiResponse<string>> AddStudent(StudentModel model)
        {
            return await _azureCosmosMongoDBRepository.AddStudent(model);
        }

        public async Task<ApiResponse<string>> UpdateStudent(StudentModel model)
        {
            return await _azureCosmosMongoDBRepository.UpdateStudent(model);
        }

        public async Task<ApiResponse<bool>> DeleteStudent(int studentId)
        {
            return await _azureCosmosMongoDBRepository.DeleteStudent(studentId);
        }
    }
}
