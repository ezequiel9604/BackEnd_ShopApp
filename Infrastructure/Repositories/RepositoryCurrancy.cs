
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryCurrancy : IRepositoryWeakDomain<Currancy>
{

    private readonly DatabaseContext _dbContext;

    public RepositoryCurrancy(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Currancy>> GetAll()
    {
        try
        {
            return await _dbContext.Currancies.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Currancy> GetById(int id)
    {
        try
        {
            return await _dbContext.Currancies.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Currancy> GetByName(string name)
    {
        try
        {
            return await _dbContext.Currancies.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

