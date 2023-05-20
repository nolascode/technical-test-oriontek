using Shared.DataTransferObjects.Address;

namespace Contracts
{
    public interface IAddress
    {
        Task InsertAsync(AddressDtoForInsert addressDtoForInsert);
        Task<AddressDtoForSelect> SelectAddressByIdAsync(int id);
        Task<ICollection<AddressDtoForSelect>> SelectAddressesListAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, AddressDtoForUpdate addressDtoForUpdate);
        Task<AddressDtoForSelect> CheckDuplicateAddress(AddressDtoForInsert addressDtoForInsert);
    }
}
