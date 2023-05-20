using Contracts;
using Dapper;
using Repository.Context;
using Repository.Queries;
using Shared.DataTransferObjects.Address;
using Shared.DataTransferObjects.Client;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace Repository
{
    internal sealed class ClientRepository : IClient
    {
        private readonly DatabaseContext _databaseContext;

        public ClientRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task InsertAsync(ClientDtoForInsert clientDtoForInsert)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            using (var transaction = connection.BeginTransaction())
            {

                await connection.ExecuteAsync(ClientQuery.InsertQuery, new
                {
                    Name = clientDtoForInsert.Name,
                    Phone = clientDtoForInsert.Phone,
                    Email = clientDtoForInsert.Email,
                    CompanyId = clientDtoForInsert.CompanyId
                });
                await transaction.CommitAsync();
            }
        }

        public async Task<ClientDtoDtoForSelect> SelectClientByNameAsync(string clientName, int companyId)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            {
                var client = await connection.QueryFirstOrDefaultAsync<ClientDtoDtoForSelect>(ClientQuery.SelectByNameAndCompanyId, 
                    new { name = clientName, companyId = companyId });
                return client;
            }
        }

        public async Task<ClientDtoDtoForSelect> SelectClientByIDAsync(int clientId)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            {
                var client = await connection.QueryFirstOrDefaultAsync<ClientDtoDtoForSelect>(ClientQuery.SelectById,
                    new { id = clientId });
                return client;
            }
        }

        public async Task<ICollection<ClientDtoDtoForSelect>> SelectClientsAsync()
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            {
                var clients = await connection.QueryAsync<ClientDtoDtoForSelect>(ClientQuery.SelectClientsList);
                return clients.ToList();
            }

        }

        public async Task UpdateAsync(int id, ClientDtoForUpdate clientDtoForUpdate)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            using (var transaction = connection.BeginTransaction())
            {
              
                await connection.ExecuteAsync(ClientQuery.UpdateClientById, 
                    new 
                    {
                        Name = clientDtoForUpdate.Name,
                        Phone = clientDtoForUpdate.Phone,
                        Email = clientDtoForUpdate.Email,
                        CompanyId = clientDtoForUpdate.CompanyId,
                        Id = id
                    });
                await transaction.CommitAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            using (var transaction = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(ClientQuery.UpdateDeletedAtById, new {id});
                await transaction.CommitAsync();
            }
        }

        public async Task<ClientWithAddressDto> SelectClientsWithAddressAsync(int clientId)
        {
            var query = ClientQuery.SelectClientsWithAddressQuery;
            using(var connection = _databaseContext.GetMySqlConnetion())
            {
                var clientDictionary = new Dictionary<int, ClientWithAddressDto>();

                var clients = await connection.QueryAsync<ClientWithAddressDto, AddressDtoForSelect, ClientWithAddressDto>
                    (query, (client, address) =>
                    {
                        if(!clientDictionary.TryGetValue(client.ClientId, out var clientEntry))
                        {
                            clientEntry = client;
                            clientDictionary.Add(client.ClientId, clientEntry);
                        }
                        if(address != null && !clientEntry.Addresses.Any(a => a.Id == client.ClientId)) 
                        {
                            clientEntry.Addresses.Add(address);
                        }
                        return clientEntry;
                    },
                    splitOn: "ClientId,id",
                    param: new { clientId  }
                    );
                return clients.First();
            }
        }
    }
}
