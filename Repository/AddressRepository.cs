using Contracts;
using Dapper;
using MySqlX.XDevAPI;
using Repository.Context;
using Repository.Queries;
using Shared.DataTransferObjects.Address;

namespace Repository
{
    internal sealed class AddressRepository : IAddress
    {
        private readonly DatabaseContext _databaseContext;

        public AddressRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task InsertAsync(AddressDtoForInsert addressDtoForInsert)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            using (var transaction = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(AddressQuery.InsertQuery, new
                {
                    Street = addressDtoForInsert.Street,
                    City = addressDtoForInsert.City,
                    State = addressDtoForInsert.State,
                    Zip = addressDtoForInsert.Zip,
                    ClientId = addressDtoForInsert.ClientId,
                });
                await transaction.CommitAsync();
            }
        }
        public async Task<ICollection<AddressDtoForSelect>> SelectAddressesListAsync()
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            {
                var addresses = await connection.QueryAsync<AddressDtoForSelect>(AddressQuery.SelectAddressesListQuery);
                return addresses.ToList();
            }
        }

        public async Task<AddressDtoForSelect> SelectAddressByIdAsync(int id)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            {
                var address = await connection.QuerySingleOrDefaultAsync<AddressDtoForSelect>(AddressQuery.SelectAddressByIdQuery, new { Id = id});
                return address;
            }
        }


        public async Task DeleteAsync(int id)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            using (var transaction = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(AddressQuery.UpdateDeletedAtByIdQuery, new { Id = id });
                await transaction.CommitAsync();
            }
        }

        public async Task UpdateAsync(int id, AddressDtoForUpdate addressDtoForUpdate)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            using (var transaction = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(AddressQuery.UpdateAddressByIdQuery, new
                {
                    Street = addressDtoForUpdate.Street,
                    City = addressDtoForUpdate.City,
                    State = addressDtoForUpdate.State,
                    Zip = addressDtoForUpdate.Zip,
                    Id = id
                });
                await transaction.CommitAsync();
            }
        }

        public async Task<AddressDtoForSelect> CheckDuplicateAddress(AddressDtoForInsert addressDtoForInsert)
        {
            using (var connection = _databaseContext.GetMySqlConnetion())
            {
                var address = await connection.QuerySingleOrDefaultAsync<AddressDtoForSelect>(AddressQuery.CheckDuplicateAddressQuery, 
                    new
                    {
                        ClientId = addressDtoForInsert.ClientId,
                        Street  = addressDtoForInsert.Street,
                        City = addressDtoForInsert.City,
                        State = addressDtoForInsert.State,
                        Zip = addressDtoForInsert.Zip
                    });
                return address;
            }
        }
    }
}
