using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Client
{
    public record class ClientDtoForInsert
    {

        [Required(ErrorMessage = "CompanyId is required")]
        public int CompanyId { get; init; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(4, ErrorMessage = "MinLength for the Name is 4")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Phone is required")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; init; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }
    }
}
