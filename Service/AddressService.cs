using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects.Address;
using Shared.DataTransferObjects.Client;

namespace Service
{
    internal sealed class AddressService : IAddressService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;

        public AddressService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }

        public async Task CreateAsync(AddressDtoForInsert addressDtoForInsert)
        {
            var client = await _repositoryManager.Client.SelectClientByIDAsync(addressDtoForInsert.ClientId);
            if(client == null)
            {
                _loggerManager.LogError($"client not found with ID = {addressDtoForInsert.ClientId}");
                throw new NotFoundException($"client not found with ID = {addressDtoForInsert.ClientId}");
            }
            var duplicateAddress = await _repositoryManager.Address.CheckDuplicateAddress(addressDtoForInsert);
            if(duplicateAddress != null) 
            {
                throw new UnprocessableEntity($"Address is duplicated ${addressDtoForInsert.ToString()}");
            }
            await _repositoryManager.Address.InsertAsync(addressDtoForInsert);
        }

        public async Task<ICollection<AddressDtoForSelect>> GetAddressesListAsync()
        {
            var addresses = await _repositoryManager.Address.SelectAddressesListAsync();
            return addresses.ToList();
        }

        public async Task<AddressDtoForSelect> GetAddressByIdAsync(int id)
        {
            var address = await FindAddressById(id);

            return address;
        }

        public async Task DeleteAsync(int id)
        {
            await FindAddressById(id);
            await _repositoryManager.Address.DeleteAsync(id);

        }

        public async Task UpdateAsync(int id, AddressDtoForUpdate addressDtoForUpdate)
        {
            var client = await _repositoryManager.Client.SelectClientByIDAsync(addressDtoForUpdate.ClientId);
            if (client == null)
            {
                _loggerManager.LogError($"client not found with ID = {addressDtoForUpdate.ClientId}");
                throw new NotFoundException($"client not found with ID = {addressDtoForUpdate.ClientId}");
            }
            await FindAddressById(id);
            await _repositoryManager.Address.UpdateAsync(id, addressDtoForUpdate);
        }


        private async Task<AddressDtoForSelect> FindAddressById(int id)
        {
            var address = await _repositoryManager.Address.SelectAddressByIdAsync(id);
            if (address == null)
            {
                _loggerManager.LogError($"Address not found with ID = {id}");
                throw new NotFoundException($"Address not found with ID = {id}");
            }
            return address;
        }


    }
}
