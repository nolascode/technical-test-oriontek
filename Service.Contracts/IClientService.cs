using Shared.DataTransferObjects.Client;
namespace Service.Contracts
{
    public interface IClientService
    {
        Task CreateAsync(ClientDtoForInsert clientDtoForInsert);
        Task<ClientDtoDtoForSelect> GetClientByIDAsync(int clientId);
        Task<ICollection<ClientDtoDtoForSelect>> GetClientsAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, ClientDtoForUpdate clientDtoForUpdate);
        Task<ClientWithAddressDto> GetClientsWithAddressAsync(int clientId);
    }
}
