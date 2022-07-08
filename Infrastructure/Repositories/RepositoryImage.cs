
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class RepositoryImage : IRepositoryImage
{
    private readonly DatabaseContext _dbContext;

    public RepositoryImage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Image> GetById(int id)
    {
        try
        {
            return await _dbContext.Images.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Image>> GetByItemId(string id)
    {
        try
        {
            var images = await _dbContext.Images.Where(x => x.ItemId == id).ToListAsync();

            return images;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Image>> GetAll()
    {
        try
        {
            return await _dbContext.Images.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Delete(Image obj)
    {
        try
        {
            _dbContext.Images.Remove(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }


    public void Update(Image obj)
    {
        try
        {
            _dbContext.Images.Update(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async void Create(Image obj)
    {
        try
        {
            await _dbContext.Images.AddAsync(obj);
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
