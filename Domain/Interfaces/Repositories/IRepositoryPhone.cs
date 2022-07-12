
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryPhone : IRepositoryStrongDomain<Phone>
{
    Task<Phone> GetById(int id);

    Task<List<Phone>> GetByClientId(string id);

}
