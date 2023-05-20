using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Client
{
    public record class ClientDtoForUpdate
    {

        [Required(ErrorMessage = "CompanyId is required")]
        public int CompanyId { get; init; }
        public string? Name { get; init; }

        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; init; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; init; }
    }
}