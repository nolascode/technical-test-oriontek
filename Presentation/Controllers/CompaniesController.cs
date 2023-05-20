using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Service.Contracts;
using Shared.DataTransferObjects.Company;

namespace Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompaniesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDtoForInsert companyDtoForInsert)
        {
            await _serviceManager.CompanyService.InsertAsync(companyDtoForInsert);
            return Created("", companyDtoForInsert);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _serviceManager.CompanyService.GetCompanyByIDAsync(id);
            return Ok(company);
        }
        [HttpGet("{id}/clients")]
        public async Task<IActionResult> GetCompanyByIdWithClients(int id)
        {
            var result = await _serviceManager.CompanyService.GetCompanyWithClientsAsync(id);
            return Ok(result);
        }
        /* [HttpGet("clients")]
         public async Task<IActionResult> GetCompanyWithClients()
         {
             var companies = await _serviceManager.CompanyService.SelectCompanyWithClientsAsync();
             return Ok(companies);
         }*/

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
             var companies = await _serviceManager.CompanyService.GetCompaniesAsync();
             return Ok(companies);
            /* var companies = await _serviceManager.CompanyService.SelectCompanyWithClientsAsync();
             return Ok(companies);*/
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id) 
        {
            await _serviceManager.CompanyService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyDtoForUpdate companyDtoForUpdate)
        {
            await _serviceManager.CompanyService.UpdateAsync(id, companyDtoForUpdate);
            return Ok();
        }



    }
}
