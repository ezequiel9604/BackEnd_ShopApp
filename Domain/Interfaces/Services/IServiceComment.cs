
using API.DTOs;

namespace Domain.Interfaces.Services;

public interface IServiceComment : IService<CommentDTO>
{
    Task<List<CommentDTO>> GetByItemId(string id);

}
