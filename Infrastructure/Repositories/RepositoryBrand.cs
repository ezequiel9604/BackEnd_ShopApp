
using Domain.Entities;
using Infrastructure.Data;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryBrand : IRepositoryWeakDomain<Brand>
{
    private readonly DatabaseContext _dbContext;

    public RepositoryBrand(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Brand>> GetAll()
    {
        try
        {
            return await _dbContext.Brands.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
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

}

