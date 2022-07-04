
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryItem : IRepository<Item>
{
    Task<Item> GetById(string id);


}