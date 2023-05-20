namespace Repository.Queries
{
    public static class CompanyQuery
    {
        public const string InsertQuery = @"INSERT INTO oriontek_test.companies (`name`,address,city,state,zip,email,phone) 
                                            VALUES (@name,@address, @city, @state, @zip, @email, @phone)";
        public const string SelectByName = @"SELECT * FROM oriontek_test.companies WHERE `name` = @name AND deleted_at IS NULL";
        public const string SelectById = @"SELECT id, `name`,address,city,state,zip,email,phone FROM oriontek_test.companies WHERE id = @id AND deleted_at IS NULL";
        public const string SelectCompanyList = @"SELECT id, `name`,address,city,state,zip,email,phone FROM oriontek_test.companies WHERE deleted_at IS NULL ORDER BY `name`";
        public const string DeleteCompanyById = @"DELETE FROM oriontek_test.companies WHERE id = @id";
        public const string UpdateDeletedAtById = @"UPDATE oriontek_test.companies SET deleted_at = NOW() WHERE id = @id";
        public const string UpdateCompanyById = @"UPDATE oriontek_test.companies 
	                                              SET   `name`    = @name, 
	                                                    address   = @address, 
                                                         city     = @city, 
                                                         state    = @state, 
                                                         zip      = @zip, 
                                                         email    = @email, 
                                                         phone    = @phone, 
	                                                    updated_at= NOW()
	                                                    WHERE id = @id";

        public const string SelectCompanyWithClientsQuery = @"SELECT comp.id AS CompanyId, comp.`name` AS CompanyName,comp.address AS CompanyAddress,comp.city AS CompanyCity,comp.state AS CompanyState,
                                                                comp.zip AS CompanyZipCode,comp.email AS companyEmail,comp.phone AS CompanyPhone, comp.created_at AS CompanyCreatedAt,
                                                                clients.id , clients.`name`, clients.email, clients.phone, clients.created_at AS createdAt
                                                                FROM oriontek_test.companies comp
                                                                LEFT JOIN oriontek_test.clients clients ON comp.id = clients.company_id AND clients.deleted_at IS NULL
                                                                WHERE comp.id = @CompanyId AND comp.deleted_at IS NULL ORDER BY comp.`name`";
    }
}