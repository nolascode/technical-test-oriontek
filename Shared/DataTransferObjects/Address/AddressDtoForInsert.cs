using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Address
{
    public record class AddressDtoForInsert
    {
        [Required(ErrorMessage = "ClientId is required")]
        public int ClientId { get; init; }

        [Required(ErrorMessage = "Street is required")]
        [MinLength(4, ErrorMessage = "MinLength for Street is 4")]
        public string Street { get; init; }

        [Required(ErrorMessage = "City is required")]
        [MinLength(4, ErrorMessage = "MinLength for City is 4")]
        public string City { get; init; }

        [Required(ErrorMessage = "State is required")]
        [MinLength(1, ErrorMessage = "MinLength for State is 1")]
        public string State { get; init; }

        [Required(ErrorMessage = "State is required")]
        [MinLength(4, ErrorMessage = "MinLength for State is 4")]
        public string Zip { get; init; }
    }
}