using Shared.DataTransferObjects.Client;
namespace Contracts
{
    public interface IClient
    {
        Task InsertAsync(ClientDtoForInsert clientDtoForInsert);
        Task<ClientDtoDtoForSelect> SelectClientByNameAsync(string clientName, int companyId);
        Task<ClientDtoDtoForSelect> SelectClientByIDAsync(int clientId);
        Task<ICollection<ClientDtoDtoForSelect>> SelectClientsAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, ClientDtoForUpdate clientDtoForUpdate);
        Task<ClientWithAddressDto> SelectClientsWithAddressAsync(int clientId);
    }
}
