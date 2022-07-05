
using AutoMapper;
using Domain.Interfaces.Services;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Services;

public class ServiceComment : IServiceComment
{

    private readonly IMapper _mapper;
    private readonly IRepositoryComment _repoComment;

    public ServiceComment(IMapper mapper, IRepositoryComment repoComment)
    {
        _mapper = mapper;
        _repoComment = repoComment;
    }

    public async Task<List<CommentDTO>> GetAll()
    {
        try
        {
            var comments = await _repoComment.GetAll();

            if(comments is null)
                return new List<CommentDTO>();

            var commentdtos = _mapper.Map<List<CommentDTO>>(comments);

            return commentdtos;

        }
        catch (Exception)
        {
            return new List<CommentDTO>();
        }
    }

    public async Task<List<CommentDTO>> GetByItemId(string itemid)
    {
        try
        {
            var comments = await _repoComment.GetAll();

            if (comments is null)
                return new List<CommentDTO>();

            var commentdtos = new List<CommentDTO>();
            foreach (var comment in comments)
            {
                if(comment.ItemId == itemid)
                {
                    commentdtos.Add(_mapper.Map<CommentDTO>(comment));
                }
            }

            return commentdtos;

        }
        catch (Exception)
        {
            return new List<CommentDTO>();
        }

    }

    public async Task<string> Create(CommentDTO commentdto)
    {
        
        if(string.IsNullOrEmpty(commentdto.Text) || string.IsNullOrEmpty(commentdto.ClientId) ||
            string.IsNullOrEmpty(commentdto.ItemId))
        {
            return "No empty allow!";
        }

        try
        {
            // creating an id to new comment
            var randomId = "CMT-" + new Random().Next(1000, 9999);
            while (true)
            {
                if (await _repoComment.GetById(randomId) is null)
                    break;

                randomId = "CMT-" + new Random().Next(1000, 9999);
            }

            var comment = _mapper.Map<Comment>(commentdto);
            comment.Id = randomId;
            comment.Date = DateTime.Now;
            comment.State = "visible";

            _repoComment.Create(comment);

            int affectedRows = await _repoComment.SaveAllChanges();

            if (affectedRows > 0)
                return "Success!";

            return "No action!";

        }
        catch (Exception)
        {
            return "Database error!";
        }

    }

    Task<string> IService<CommentDTO>.Update(CommentDTO obj)
    {
        throw new NotImplementedException();
    }

    Task<string> IService<CommentDTO>.Delete(string obj)
    {
        throw new NotImplementedException();
    }
}