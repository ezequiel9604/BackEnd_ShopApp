
using Domain.Entities;
using Infrastructure.Data;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryCategory : IRepositoryWeakDomain<Category>
{
    private readonly DatabaseContext _dbContext;

    public RepositoryCategory(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Category>> GetAll()
    {
        try
        {
            return await _dbContext.Categories.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
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

}

