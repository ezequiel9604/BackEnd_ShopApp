
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryLanguage : IRepositoryWeakDomain<Language>
{

    private readonly DatabaseContext _dbContext;
    public RepositoryLanguage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Language>> GetAll()
    {
        try
        {
            return await _dbContext.Languages.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Language> GetById(int id)
    {
        try
        {
            return await _dbContext.Languages.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Language> GetByName(string name)
    {
        try
        {
            return await _dbContext.Languages.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

