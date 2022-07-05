
using Domain.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{

    private readonly IServiceComment _servComment;

    public CommentController(IServiceComment servComment)
    {
        _servComment = servComment;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<CommentDTO>>> GetAll()
    {
        return await _servComment.GetAll();
    }

    [HttpGet("GetByItemId")]
    public async Task<ActionResult<List<CommentDTO>>> GetByItemId(string id)
    {
        return await _servComment.GetByItemId(id);
    }

}