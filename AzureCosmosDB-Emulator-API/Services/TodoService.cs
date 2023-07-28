using AzureCosmosDB_Emulator_API.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace AzureCosmosDB_Emulator_API.Services
{
    public class TodoService : ITodoService
    {
        private readonly AzureCosmosDBSettings _azureCosmosDBSettings;
        public TodoService(IOptions<AzureCosmosDBSettings> azureCosmosDBSettings)
        {
            _azureCosmosDBSettings = azureCosmosDBSettings.Value;
        }
        private Container ContainerClient()
        {
            CosmosClient cosmosDbClient = new CosmosClient(_azureCosmosDBSettings.URI, _azureCosmosDBSettings.PrimaryKey);
            Container containerClient = cosmosDbClient.GetContainer(_azureCosmosDBSettings.DatabaseName, _azureCosmosDBSettings.ContainerName);
            return containerClient;
        }
        public async Task<Tasks> AddTask(Tasks task)
        {
            var _container = ContainerClient();
            var item = await _container.CreateItemAsync<Tasks>(task, new PartitionKey(task.id));
            return item;
        }
        public async Task DeleteTask(string id, string partition)
        {
            var _container = ContainerClient();
            await _container.DeleteItemAsync<Tasks>(id, new PartitionKey(partition));
        }
        public async Task<List<Tasks>> GetTasks(string cosmosQuery)
        {
            var _container = ContainerClient();
            var query = _container.GetItemQueryIterator<Tasks>(new QueryDefinition(cosmosQuery));
            List<Tasks> results = new List<Tasks>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }
            return results;
        }
        public async Task<Tasks> UpdateTask(Tasks task)
        {
            var _container = ContainerClient();
            var item = await _container.UpsertItemAsync<Tasks>(task, new PartitionKey(task.id));
            return item;
        }
    }
}
