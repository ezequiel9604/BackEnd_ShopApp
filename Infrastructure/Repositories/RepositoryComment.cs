
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class RepositoryComment : IRepositoryComment
{

    private readonly DatabaseContext _dbContext;

    public RepositoryComment(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Comment>> GetAll()
    {
        try
        {
            var comments = await _dbContext.Comments.ToListAsync();

            return comments;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Comment> GetById(string id)
    {
        try
        {
            var comment = await _dbContext.Comments.Where(x => x.Id == id).FirstOrDefaultAsync();

            return comment;

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Comment>> GetByItemId(string id)
    {
        try
        {
            var comments = await _dbContext.Comments.Where(x => x.ItemId == id).ToListAsync();

            return comments;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Comment>> GetByClientId(string id)
    {
        try
        {
            var comments = await _dbContext.Comments.Where(x => x.ClientId == id).ToListAsync();

            return comments;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async void Create(Comment obj)
    {
        try
        {
            await _dbContext.Comments.AddAsync(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Update(Comment obj)
    {
        try
        {
            _dbContext.Comments.Update(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Delete(Comment obj)
    {
        try
        {
            _dbContext.Comments.Remove(obj);
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