using Shared.DataTransferObjects.Address;

namespace Shared.DataTransferObjects.Client
{
    public record ClientDtoDtoForSelect
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public DateTime CreatedAt { get; init; }

    }
}

