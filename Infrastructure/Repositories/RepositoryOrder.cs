
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class RepositoryOrder : IRepositoryOrder
{

    private readonly DatabaseContext _dbContext;

    public RepositoryOrder(DatabaseContext databaseContext)
    {
        _dbContext = databaseContext;
    }

    public async Task<List<Order>> GetAll()
    {

        try
        {
            var orders = await _dbContext.Orders.ToListAsync();

            return orders;

        }
        catch (Exception)
        {
            throw;
        }

    }

    public async void Create(Order obj)
    {

        try
        {
            await _dbContext.Orders.AddAsync(obj);
        }
        catch (Exception)
        {
            throw;
        }

    }

    public void Delete(Order obj)
    {
        try
        {
            _dbContext.Orders.Remove(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Update(Order obj)
    {
        try
        {
            _dbContext.Entry<Order>(obj).State = EntityState.Modified;
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