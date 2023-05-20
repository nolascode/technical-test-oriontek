namespace Repository.Queries
{
    public static class ClientQuery
    {
        public const string InsertQuery = @"INSERT INTO oriontek_test.clients (`name`, phone, email, company_id) 
                                            VALUES (@Name, @Phone, @Email, @CompanyId)";

        public const string SelectByNameAndCompanyId = @"SELECT * FROM oriontek_test.clients WHERE `name` = @name AND deleted_at IS NULL AND company_id = @companyId";

        public const string SelectById = @"SELECT id, `name`, phone,email, company_id, created_at AS createdAt FROM oriontek_test.clients WHERE id = @id AND deleted_at IS NULL";
        public const string SelectClientsList = @"SELECT id, `name`, phone,email, company_id, created_at AS createdAt FROM oriontek_test.clients WHERE deleted_at IS NULL ORDER BY `name`";
        public const string DeleteClientById = @"DELETE FROM oriontek_test.clients WHERE id = @id";
        public const string UpdateDeletedAtById = @"UPDATE oriontek_test.clients SET deleted_at = NOW() WHERE id = @id";
        public const string UpdateClientById = @"UPDATE
                                                  oriontek_test.clients
                                                    SET
                                                      `name` = @Name,
                                                      phone = @Phone,
                                                      email = @Email,
                                                      company_id = @CompanyId,
                                                      updated_at = NOW()
                                                    WHERE id = @Id";

        public const string SelectClientsWithAddressQuery = @"SELECT clients.id AS ClientId, clients.name AS clientName, clients.email AS clientEmail, clients.phone AS clientPhone, clients.created_at clientCreatedAt,
                                                                adds.id,adds.street, adds.city, adds.state, adds.zip,  adds.created_at createdAt 
                                                                FROM oriontek_test.clients clients 
                                                                LEFT JOIN oriontek_test.addresses adds ON clients.id = adds.client_id AND adds.deleted_at IS NULL 
                                                                WHERE clients.id = @clientId AND clients.deleted_at IS NULL ORDER BY `name`";

    }
}