using Shared.DataTransferObjects.Company;

namespace Contracts
{
    public interface ICompany
    {
        Task InsertAsync(CompanyDtoForInsert companyDtoForInsert);
        Task<CompanyDtoForSelect> SelectCompanyByNameAsync(string companyName);
        Task<CompanyDtoForSelect> SelectCompanyByIDAsync(int companyId);
        Task<ICollection<CompanyDtoForSelect>> SelectCompaniesAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, CompanyDtoForUpdate companyDtoForUpdate);
        Task<CompanyWithClientsDto> SelectCompanyWithClientsAsync(int companyId);
    }
}
