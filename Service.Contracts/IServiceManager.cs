namespace Service.Contracts
{
    public interface IServiceManager
    {
        ICompanyService CompanyService { get; }
        IClientService ClientService { get; }
        IAddressService AddressService { get; }
    }
}
