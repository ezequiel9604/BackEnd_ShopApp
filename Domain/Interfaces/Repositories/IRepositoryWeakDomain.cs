
namespace Domain.Interfaces.Repositories;

public interface IRepositoryWeakDomain<T>
{
    Task<List<T>> GetAll();

    Task<T> GetById(int id);

    Task<T> GetByName(string name);

}

