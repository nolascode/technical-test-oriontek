namespace Shared.DataTransferObjects.Address
{
    public record AddressDtoForSelect
    {
        //public int ClientId { get; init; }
        public int Id { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string Zip { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}

