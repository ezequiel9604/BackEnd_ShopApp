
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class RepositorySubitem : IRepositorySubitem
{
    private readonly DatabaseContext _dbContext;

    public RepositorySubitem(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SubItem> GetById(int id)
    {
        try
        {
            return await _dbContext.SubItems.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async Task<List<SubItem>> GetByItemId(string id)
    {
        try
        {
            var subitems = await _dbContext.SubItems.Where(x => x.ItemId == id).ToListAsync();

            return subitems;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<SubItem>> GetAll()
    {
        try
        {
            return await _dbContext.SubItems.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Delete(SubItem obj)
    {
        try
        {
            _dbContext.SubItems.Remove(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }


    public void Update(SubItem obj)
    {
        try
        {
            _dbContext.SubItems.Update(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async void Create(SubItem obj)
    {
        try
        {
            await _dbContext.SubItems.AddAsync(obj);
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
