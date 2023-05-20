using Shared.DataTransferObjects.Client;

namespace Shared.DataTransferObjects.Company
{
    public record CompanyDtoForSelect
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string Zip { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}

