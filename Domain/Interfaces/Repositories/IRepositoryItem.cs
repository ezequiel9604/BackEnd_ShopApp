
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryItem : IRepositoryStrongDomain<Item>
{
    Task<Item> GetById(string id);


}