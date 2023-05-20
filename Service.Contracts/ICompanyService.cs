using Shared.DataTransferObjects.Company;
namespace Service.Contracts
{
    public interface ICompanyService
    {
        Task InsertAsync(CompanyDtoForInsert companyDtoForInsert);
        Task<CompanyDtoForSelect> GetCompanyByIDAsync(int companyId);
        Task<ICollection<CompanyDtoForSelect>> GetCompaniesAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, CompanyDtoForUpdate companyDtoForUpdate);
        Task<CompanyWithClientsDto> GetCompanyWithClientsAsync(int companyId);
    }
}
