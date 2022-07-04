
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryClient : IRepository<Client>
{

    Task<Client> GetById(string id);

    Task<Client> GetByEmail(string email);

}
