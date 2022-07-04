
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class RepositoryPhone : IRepositoryPhone
{
    private readonly DatabaseContext _dbContext;

    public RepositoryPhone(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Phone> GetById(int id)
    {
        try
        {
            return await _dbContext.Phones.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async Task<List<Phone>> GetByClientId(string id)
    {
        try
        {
            var phones = await _dbContext.Phones.Where(x => x.ClientId == id).ToListAsync();

            return phones;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Phone>> GetAll()
    {
        try
        {
            return await _dbContext.Phones.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Delete(Phone obj)
    {
        try
        {
            _dbContext.Phones.Remove(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }


    public void Update(Phone obj)
    {
        try
        {
            _dbContext.Phones.Update(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async void Create(Phone obj)
    {
        try
        {
            await _dbContext.Phones.AddAsync(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> SaveAllChanges()
    {
        try
        {
            return await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }

    }
}
