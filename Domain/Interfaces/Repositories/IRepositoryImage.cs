
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryImage : IRepository<Image>
{
    Task<Image> GetById(int id);

    Task<List<Image>> GetByItemId(string id);

}
