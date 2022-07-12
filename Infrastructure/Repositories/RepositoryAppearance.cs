
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryAppearance : IRepositoryWeakDomain<Appearance>
{

    private readonly DatabaseContext _dbContext;

    public RepositoryAppearance(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Appearance>> GetAll()
    {
        try
        {
            return await _dbContext.Appearances.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Appearance> GetById(int id)
    {
        try
        {
            return await _dbContext.Appearances.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Appearance> GetByName(string name)
    {
        try
        {
            return await _dbContext.Appearances.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

