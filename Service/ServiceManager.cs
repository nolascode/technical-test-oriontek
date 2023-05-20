using Contracts;
using Service.Contracts;
namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;

        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IClientService> _clientService;
        private readonly Lazy<IAddressService> _addressService; 

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;

            _companyService = new Lazy<ICompanyService>(() => new CompanyService(_repositoryManager, logger));
            _clientService = new Lazy<IClientService>(() => new ClientService(_repositoryManager, logger));
            _addressService = new Lazy<IAddressService>(() => new AddressService(_repositoryManager, logger));
        }
        public ICompanyService CompanyService => _companyService.Value;
        public IClientService ClientService => _clientService.Value;
        public IAddressService AddressService => _addressService.Value;
    }
}