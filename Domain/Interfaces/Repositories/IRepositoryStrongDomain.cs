
namespace Domain.Interfaces.Repositories;

public interface IRepositoryStrongDomain<T>
{
    Task<List<T>> GetAll();

    void Create(T obj);

    void Delete(T obj);

    void Update(T obj);

    Task<int> SaveAllChanges();
}
