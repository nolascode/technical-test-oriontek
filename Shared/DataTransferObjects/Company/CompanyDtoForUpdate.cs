using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Company
{
    public record CompanyDtoForUpdate
    {
        public string? Name { get; init; }
        public string? Address { get; init; }
        public string? City { get; init; }
        public string? State { get; init; }
        public string? Zip { get; init; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; init; }

        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; init; }
    }
}

