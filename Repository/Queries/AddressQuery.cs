namespace Repository.Queries
{
    public static class AddressQuery
    {
        public const string InsertQuery = @"INSERT INTO oriontek_test.addresses (street,city,state,zip,client_id) VALUES (@Street, @City, @State, @Zip, @ClientId)";
        public const string SelectAddressesListQuery = @"SELECT
                                                      id, street, city, state, zip, client_id clientId,  created_at createdAt 
                                                      FROM oriontek_test.addresses
                                                      WHERE deleted_at IS NULL ";

        public const string SelectAddressByIdQuery = @"SELECT
                                                      id, street, city, state, zip, client_id clientId,  created_at createdAt 
                                                      FROM oriontek_test.addresses
                                                      WHERE deleted_at IS NULL AND id = @Id";
        public const string UpdateDeletedAtByIdQuery = @"UPDATE oriontek_test.addresses SET deleted_at = NOW() WHERE id = @Id";
        public const string UpdateAddressByIdQuery = @"UPDATE oriontek_test.addresses SET street = @Street, city = @City, state = @State, zip = @Zip WHERE id = @Id";
        public const string CheckDuplicateAddressQuery = @"SELECT id,street, city, state, zip, client_id clientId, created_at createdAt
                                                            FROM oriontek_test.addresses
                                                            WHERE deleted_at IS NULL
                                                            AND client_id = @ClientId
                                                            AND street = @Street
                                                            AND city = @City
                                                            AND state = @State
                                                            AND zip = @Zip";
    }
}