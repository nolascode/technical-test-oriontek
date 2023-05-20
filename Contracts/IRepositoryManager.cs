namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompany Company { get; }
        IClient Client { get; }
        IAddress Address { get; }
    }
}
