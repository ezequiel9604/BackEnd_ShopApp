
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositorySubitem : IRepositoryStrongDomain<SubItem>
{
    Task<SubItem> GetById(int id);

    Task<List<SubItem>> GetByItemId(string id);

}
