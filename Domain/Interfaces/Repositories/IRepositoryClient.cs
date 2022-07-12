
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryClient : IRepositoryStrongDomain<Client>
{

    Task<Client> GetById(string id);

    Task<Client> GetByEmail(string email);

}
