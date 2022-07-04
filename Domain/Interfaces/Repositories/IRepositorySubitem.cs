
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositorySubitem : IRepository<SubItem>
{
    Task<SubItem> GetById(int id);

    Task<List<SubItem>> GetByItemId(string id);

}
