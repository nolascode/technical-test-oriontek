using Shared.DataTransferObjects.Client;
namespace Shared.DataTransferObjects.Company
{
    public record CompanyWithClientsDto
    {
        public int CompanyId { get; init; }
        public string CompanyName { get; init; }
        public string CompanyAddress { get; init; }
        public string CompanyCity { get; init; }
        public string CompanyState { get; init; }
        public string CompanyZipCode { get; init; }
        public string CompanyEmail { get; init; }
        public string CompanyPhone { get; init; }
        public string CompanyCreatedAt { get; init; }
        public ICollection<ClientDtoDtoForSelect> Clients { get; init;} = new List<ClientDtoDtoForSelect>();
    }

}
