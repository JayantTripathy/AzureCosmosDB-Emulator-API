using Newtonsoft.Json;

namespace AzureCosmosDB_Emulator_API.Models
{
    public class Tasks
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
