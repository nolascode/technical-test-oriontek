using Contracts;
using Dapper;
using Repository.Context;
using Repository.Queries;
using Shared.DataTransferObjects.Client;
using Shared.DataTransferObjects.Company;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;

namespace Repository
{
    internal sealed class CompanyRepository : ICompany
    {
        private readonly DatabaseContext _databaseContext;

        public CompanyRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<CompanyDtoForSelect> SelectCompanyByIDAsync(int companyId)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            {
                var company = await connection.QueryFirstOrDefaultAsync<CompanyDtoForSelect>(CompanyQuery.SelectById, 
                    new { id = companyId });
                return company;
            }
        }

        public async Task<CompanyDtoForSelect> SelectCompanyByNameAsync(string companyName)
        {
            using(var connection = _databaseContext.GetMySqlConnetion())
            {
                var company = await connection.QueryFirstOrDefaultAsync<CompanyDtoForSelect>(CompanyQuery.SelectByName, new { name = companyName });
                return company;
            }
        }

        public async Task InsertAsync(CompanyDtoForInsert companyDtoForInsert)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            using(var transaction = connection.BeginTransaction())
            {
                var parameters = new DynamicParameters(companyDtoForInsert);
                await connection.ExecuteAsync(CompanyQuery.InsertQuery, parameters);
                await transaction.CommitAsync();
            }
        }

        public async Task<ICollection<CompanyDtoForSelect>> SelectCompaniesAsync()
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            {
                var companies = await connection.QueryAsync<CompanyDtoForSelect>(CompanyQuery.SelectCompanyList);
                return companies.ToList();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            using (var transaction = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(CompanyQuery.UpdateDeletedAtById, new {id});
                await transaction.CommitAsync();
            }
        }

        public async Task UpdateAsync(int id, CompanyDtoForUpdate companyDtoForUpdate)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            using (var transaction = connection.BeginTransaction())
            {
                var parameters = new DynamicParameters(companyDtoForUpdate);
                parameters.Add("id", id);
                await connection.ExecuteAsync(CompanyQuery.UpdateCompanyById, parameters);
                await transaction.CommitAsync();
            }
        }

        public async Task<CompanyWithClientsDto> SelectCompanyWithClientsAsync(int companyId)
        {
            var query = CompanyQuery.SelectCompanyWithClientsQuery;
            using(var connection = _databaseContext.GetMySqlConnetion())
            {
                var companyDictionary = new Dictionary<int, CompanyWithClientsDto>();
                var companies = await connection.QueryAsync<CompanyWithClientsDto, ClientDtoDtoForSelect, CompanyWithClientsDto>
                    (query, (company, client) =>
                    {
                        if(!companyDictionary.TryGetValue(company.CompanyId, out var companyEntry))
                        {
                            companyEntry = company;
                            companyDictionary.Add(company.CompanyId, companyEntry);
                        }
                        if (client != null && !companyEntry.Clients.Any(c => c.Id == client.Id))
                        {
                            companyEntry.Clients.Add(client);
                        }
                        return companyEntry;
                    },
                splitOn: "CompanyId, id",
                    param: new { CompanyId = companyId }
                    );
                return companies.First();
            }
        }
    }
}
