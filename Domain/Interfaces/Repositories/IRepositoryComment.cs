
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryComment : IRepositoryStrongDomain<Comment>
{
    Task<Comment> GetById(string id);

    Task<List<Comment>> GetByItemId(string id);

    Task<List<Comment>> GetByClientId(string id);
}

