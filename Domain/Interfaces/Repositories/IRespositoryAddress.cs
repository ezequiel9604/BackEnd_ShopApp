
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryAddress : IRepository<Address>
{
    Task<Address> GetById(int id);

    Task<List<Address>> GetByClientId(string id);

}
