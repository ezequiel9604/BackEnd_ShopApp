
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryAddress : IRepositoryStrongDomain<Address>
{
    Task<Address> GetById(int id);

    Task<List<Address>> GetByClientId(string id);

}
