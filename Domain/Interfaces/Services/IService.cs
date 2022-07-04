
namespace Domain.Interfaces.Services;

public interface IService<T>
{
    Task<List<T>> GetAll();

    Task<string> Create(T obj);

    Task<string> Update(T obj);

    Task<string> Delete(string obj);

}