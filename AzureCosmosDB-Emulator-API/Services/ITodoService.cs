using AzureCosmosDB_Emulator_API.Models;

namespace AzureCosmosDB_Emulator_API.Services
{
    public interface ITodoService
    {
        Task<List<Tasks>> GetTasks(string cosmosQuery);
        Task<Tasks> AddTask(Tasks task);
        Task<Tasks> UpdateTask(Tasks task);
        Task DeleteTask(string id, string partition);
    }
}
