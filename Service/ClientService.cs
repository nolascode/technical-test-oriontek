using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects.Company;
using Entities.Exceptions;
using System.ComponentModel.Design;
using Shared.DataTransferObjects.Client;

namespace Service
{
    internal sealed class ClientService : IClientService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;

        public ClientService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }

        public async Task CreateAsync(ClientDtoForInsert clientDtoForInsert)
        {
            var companyExists = await _repositoryManager.Company.SelectCompanyByIDAsync(clientDtoForInsert.CompanyId);
            if (companyExists == null)
            {
                _loggerManager.LogError($"Company not found with ID = {clientDtoForInsert.CompanyId}");
                throw new NotFoundException($"Company not found with ID = {clientDtoForInsert.CompanyId}");
            }

            var checkExistence = await _repositoryManager.Client.SelectClientByNameAsync(clientDtoForInsert.Name, clientDtoForInsert.CompanyId);
            if (checkExistence != null)
            {
                _loggerManager.LogError($"Client's name already exists = {clientDtoForInsert.Name}");
                throw new UnprocessableEntity("Client's name already exists");
            }
            _loggerManager.LogInfo($"Client created successfully = {clientDtoForInsert.ToString()}");
            await _repositoryManager.Client.InsertAsync(clientDtoForInsert);
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _repositoryManager.Client.SelectClientByIDAsync(id);
            if (company == null)
            {
                _loggerManager.LogError($"Client not found with ID = {id}");
                throw new NotFoundException($"Client not found with ID = {id}");
            }
            _loggerManager.LogInfo($"Returning client with iID = {id}");
            await _repositoryManager.Client.DeleteAsync(id);
        }


        public async Task<ClientDtoDtoForSelect> GetClientByIDAsync(int clientId)
        {
            var client = await _repositoryManager.Client.SelectClientByIDAsync(clientId);
            if (client == null)
            {
                _loggerManager.LogError($"Client not found with ID = {clientId}");
                throw new NotFoundException($"Client not found with ID = {clientId}");
            }
            _loggerManager.LogInfo($"Returning client with iID = {clientId}");
            return client;
        }


        public async Task<ICollection<ClientDtoDtoForSelect>> GetClientsAsync()
        {
            var clients = await _repositoryManager.Client.SelectClientsAsync();
            _loggerManager.LogInfo($"Getting clients Async");
            return clients;
        }

        public async Task<ClientWithAddressDto> GetClientsWithAddressAsync(int clientId)
        {
            var checkExistence = await _repositoryManager.Client.SelectClientByIDAsync(clientId);
            if (checkExistence == null)
            {
                _loggerManager.LogError($"Client not found with ID = {clientId}");
                throw new NotFoundException($"Client not found with ID = {clientId}");
            }
            var result = await _repositoryManager.Client.SelectClientsWithAddressAsync(clientId);
            return result;
        }

        public async Task UpdateAsync(int id, ClientDtoForUpdate clientDtoForUpdate)
        {
            var companyExists = await _repositoryManager.Company.SelectCompanyByIDAsync(clientDtoForUpdate.CompanyId);
            if (companyExists == null)
            {
                _loggerManager.LogError($"Company not found with ID = {clientDtoForUpdate.CompanyId}");
                throw new NotFoundException($"Company not found with ID = {clientDtoForUpdate.CompanyId}");
            }


            var checkExistence = await _repositoryManager.Client.SelectClientByIDAsync(id);
            if (checkExistence == null)
            {
                _loggerManager.LogError($"Client not found with ID = {id}");
                throw new NotFoundException($"Client not found with ID = {id}");
            }
            await _repositoryManager.Client.UpdateAsync(id, clientDtoForUpdate);
        }
    }
}
