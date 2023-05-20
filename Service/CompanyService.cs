using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects.Company;
using Entities.Exceptions;
using System.ComponentModel.Design;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }

        public async Task DeleteAsync(int companyId)
        {
            var company = await _repositoryManager.Company.SelectCompanyByIDAsync(companyId);
            if (company == null)
            {
                _loggerManager.LogError($"Company not found with ID = {companyId}");
                throw new NotFoundException($"Company not found with ID = {companyId}");
            }
            _loggerManager.LogInfo($"Returning company with iID = {companyId}");
            await _repositoryManager.Company.DeleteAsync(companyId);
        }

        public async Task<ICollection<CompanyDtoForSelect>> GetCompaniesAsync()
        {
            var companies = await _repositoryManager.Company.SelectCompaniesAsync();
            _loggerManager.LogInfo($"Getting Companies Async");
            return companies;
        }

        public async Task<CompanyDtoForSelect> GetCompanyByIDAsync(int companyId)
        {
            var company = await _repositoryManager.Company.SelectCompanyByIDAsync(companyId);
            if(company == null)
            {
                _loggerManager.LogError($"Company not found with ID = {companyId}");
                throw new NotFoundException($"Company not found with ID = {companyId}");
            }
            _loggerManager.LogInfo($"Returning company with iID = {companyId}");
            return company;
        }

        public async Task<CompanyWithClientsDto> GetCompanyWithClientsAsync(int companyId)
        {
            var result = await _repositoryManager.Company.SelectCompanyWithClientsAsync(companyId);
            return result;
        }

        public async Task InsertAsync(CompanyDtoForInsert companyDtoForInsert)
        {
            var checkExistence = await _repositoryManager.Company.SelectCompanyByNameAsync(companyDtoForInsert.Name);
            if (checkExistence != null)
            {
                _loggerManager.LogError($"Company's name already exists = {companyDtoForInsert.Name}");
                throw new UnprocessableEntity("Company's name already exists");
            }
            _loggerManager.LogInfo($"Company created successfully = {companyDtoForInsert.ToString()}");
            await _repositoryManager.Company.InsertAsync(companyDtoForInsert);
        }

        public async Task UpdateAsync(int id, CompanyDtoForUpdate companyDtoForUpdate)
        {
            var checkExistence = await _repositoryManager.Company.SelectCompanyByIDAsync(id);
            if (checkExistence == null)
            {
                _loggerManager.LogError($"Company not found with ID = {id}");
                throw new NotFoundException($"Company not found with ID = {id}");
            }
            await _repositoryManager.Company.UpdateAsync(id, companyDtoForUpdate);
        }
    }
}
