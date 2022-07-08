
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryCategory : IRepository<Category>
{
    Task<Category> GetByName(string name);

    Task<Category> GetById(int id);

}

