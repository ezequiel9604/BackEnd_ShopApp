
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryClientType : IRepositoryWeakDomain<ClientType>
{

    private readonly DatabaseContext _dbContext;
    public RepositoryClientType(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ClientType>> GetAll()
    {
        try
        {
            return await _dbContext.Types.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ClientType> GetById(int id)
    {
        try
        {
            return await _dbContext.Types.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ClientType> GetByName(string name)
    {
        try
        {
            return await _dbContext.Types.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

