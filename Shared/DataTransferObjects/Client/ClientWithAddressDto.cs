using Shared.DataTransferObjects.Address;

namespace Shared.DataTransferObjects.Client
{
    public record ClientWithAddressDto
    {
        public int ClientId { get; init; }
        public string ClientName { get; init; }
        public string ClientEmail { get; init; }
        public string ClientPhone { get; init; }
        public DateTime ClientCreatedAt { get; init; }
        public ICollection<AddressDtoForSelect> Addresses { get; init; } = new List<AddressDtoForSelect>();

    }
}
