
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryState : IRepositoryWeakDomain<State>
{

    private readonly DatabaseContext _dbContext;
    public RepositoryState(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<State>> GetAll()
    {
        try
        {
            return await _dbContext.States.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<State> GetById(int id)
    {
        try
        {
            return await _dbContext.States.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<State> GetByName(string name)
    {
        try
        {
            return await _dbContext.States.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

