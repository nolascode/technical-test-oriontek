using Contracts;
using Repository.Context;
namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Lazy<ICompany> _companyRepository;
        private readonly Lazy<IClient> _clientRepository;
        private readonly Lazy<IAddress> _addressRepository;

        public RepositoryManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _companyRepository = new Lazy<ICompany>(() => new CompanyRepository(_databaseContext));
            _clientRepository = new Lazy<IClient>(() => new ClientRepository(_databaseContext));
            _addressRepository = new Lazy<IAddress>(() => new AddressRepository(_databaseContext));
        }
        public ICompany Company => _companyRepository.Value;
        public IClient Client => _clientRepository.Value;
        public IAddress Address => _addressRepository.Value;
    }
}
