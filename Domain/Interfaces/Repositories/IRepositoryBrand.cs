
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryBrand : IRepository<Brand>
{
    Task<Brand> GetByName(string name);

    Task<Brand> GetById(int id);
}


