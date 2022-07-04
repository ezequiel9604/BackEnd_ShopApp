
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class RepositoryItem : IRepositoryItem
{
    private readonly DatabaseContext _dbContext;

    public RepositoryItem(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Item>> GetAll()
    {
        try
        {
            return await _dbContext.Items.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Item> GetById(string id)
    {
        try
        {
            return await _dbContext.Items.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Delete(Item obj)
    {
        try
        {
            _dbContext.Items.Remove(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Update(Item obj)
    {
        try
        {
            _dbContext.Items.Update(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async void Create(Item obj)
    {
        try
        {
            await _dbContext.Items.AddAsync(obj);
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