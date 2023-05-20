using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Service.Contracts;
using Shared.DataTransferObjects.Client;
using Shared.DataTransferObjects.Company;

namespace Presentation.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ClientsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientDtoForInsert clientDtoForInsert)
        {
            await _serviceManager.ClientService.CreateAsync(clientDtoForInsert);
            return Created("", clientDtoForInsert);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _serviceManager.ClientService.GetClientByIDAsync(id);
            return Ok(client);
        }

        [HttpGet("{id}/addresses")]
        public async Task<IActionResult> GetClientByIdWithAddress(int id)
        {
            var resutls = await _serviceManager.ClientService.GetClientsWithAddressAsync(id);
            return Ok(resutls);
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _serviceManager.ClientService.GetClientsAsync();
            return Ok(clients);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id) 
        {
            await _serviceManager.ClientService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDtoForUpdate clientDtoForUpdate)
        {
            await _serviceManager.ClientService.UpdateAsync(id, clientDtoForUpdate);
            return Ok();
        }

    }
}
