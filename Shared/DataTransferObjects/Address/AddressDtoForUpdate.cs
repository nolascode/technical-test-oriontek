using System.ComponentModel.DataAnnotations;
namespace Shared.DataTransferObjects.Address
{
    public record AddressDtoForUpdate
    {
        [Required(ErrorMessage = "ClientId is required")]
        public int ClientId { get; init; }
        public string? Street { get; init; }
        public string? City { get; init; }
        public string? State { get; init; }
        public string? Zip { get; init; }
    }
}