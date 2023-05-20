using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Company
{
    public record CompanyDtoForInsert
    {

        [Required(ErrorMessage = "Name is required")]
        [MinLength(4, ErrorMessage = "MinLength for the Name is 4")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Address is required")]
        [MinLength(10, ErrorMessage = "MinLength for Address is 10")]
        public string Address { get; init; }

        [Required(ErrorMessage = "City is required")]
        [MinLength(4, ErrorMessage = "Min Length for City is 4")]
        public string City { get; init; }

        [Required(ErrorMessage = "State is required")]
        [MinLength(2, ErrorMessage = "Min Length for State is 2")]
        public string State { get; init; }

        [Required(ErrorMessage = "Zip is required")]
        [MinLength(5, ErrorMessage = "Min Length for Zip is 5")]
        public string Zip { get; init; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }

        [Required(ErrorMessage = "Phone is required")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; init; }

    }
}
