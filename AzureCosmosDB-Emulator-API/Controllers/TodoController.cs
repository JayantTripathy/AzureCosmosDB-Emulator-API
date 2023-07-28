using AzureCosmosDB_Emulator_API.Models;
using AzureCosmosDB_Emulator_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureCosmosDB_Emulator_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sqlQuery = "SELECT * FROM c";
            var result = await _todoService.GetTasks(sqlQuery);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Tasks task)
        {
            task.id = Guid.NewGuid().ToString();
            var result = await _todoService.AddTask(task);

            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Tasks task)
        {
            var result = await _todoService.UpdateTask(task);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id, string
        partition)
        {
            await _todoService.DeleteTask(id, partition);
            return Ok();
        }
    }
}
