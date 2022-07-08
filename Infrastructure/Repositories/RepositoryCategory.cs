
using Domain.Entities;
using Infrastructure.Data;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryCategory : IRepositoryCategory
{
    private readonly DatabaseContext _dbContext;

    public RepositoryCategory(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> GetByName(string name)
    {
        try
        {
            return await _dbContext.Categories.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<Category> GetById(int id)
    {
        try
        {
            return await _dbContext.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    void IRepository<Category>.Create(Category obj)
    {
        throw new NotImplementedException();
    }

    void IRepository<Category>.Delete(Category obj)
    {
        throw new NotImplementedException();
    }

    Task<List<Category>> IRepository<Category>.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<int> IRepository<Category>.SaveAllChanges()
    {
        throw new NotImplementedException();
    }

    void IRepository<Category>.Update(Category obj)
    {
        throw new NotImplementedException();
    }
}

