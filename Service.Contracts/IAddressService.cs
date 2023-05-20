using Shared.DataTransferObjects.Address;
namespace Service.Contracts
{
    public interface IAddressService
    {
        Task CreateAsync(AddressDtoForInsert addressDtoForInsert);
        Task<AddressDtoForSelect> GetAddressByIdAsync(int id);
        Task<ICollection<AddressDtoForSelect>> GetAddressesListAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, AddressDtoForUpdate addressDtoForUpdate);
    }
}