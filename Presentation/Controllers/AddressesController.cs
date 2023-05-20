using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Address;
using Shared.DataTransferObjects.Client;

namespace Presentation.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AddressesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressDtoForInsert addressDtoForInsert)
        {
            await _serviceManager.AddressService.CreateAsync(addressDtoForInsert);
            return Created("", addressDtoForInsert);
        }

        [HttpGet]
        public async Task<IActionResult> GetAddressesList()
        {
            var addresses = await _serviceManager.AddressService.GetAddressesListAsync();
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var address = await _serviceManager.AddressService.GetAddressByIdAsync(id);
            return Ok(address);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            await _serviceManager.AddressService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressDtoForUpdate addressDtoForUpdate)
        {
            await _serviceManager.AddressService.UpdateAsync(id, addressDtoForUpdate);
            return Ok();
        }
    }
}
