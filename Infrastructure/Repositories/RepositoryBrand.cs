
using Domain.Entities;
using Infrastructure.Data;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryBrand : IRepositoryBrand
{
    private readonly DatabaseContext _dbContext;

    public RepositoryBrand(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Brand> GetByName(string name)
    {
        try
        {
            return await _dbContext.Brands.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<Brand> GetById(int id)
    {
        try
        {
            return await _dbContext.Brands.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    void IRepository<Brand>.Create(Brand obj)
    {
        throw new NotImplementedException();
    }

    void IRepository<Brand>.Delete(Brand obj)
    {
        throw new NotImplementedException();
    }

    Task<List<Brand>> IRepository<Brand>.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<int> IRepository<Brand>.SaveAllChanges()
    {
        throw new NotImplementedException();
    }

    void IRepository<Brand>.Update(Brand obj)
    {
        throw new NotImplementedException();
    }
}

