
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Interfaces.Services;
using Domain.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{

    private readonly IServiceItem _servItem;

    public ItemController(IServiceItem servItem)
    {
        _servItem = servItem;
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ItemDTO>>> GetAll()
    {
        var itemDtos = await _servItem.GetAll();

        return itemDtos;
    }


    [HttpPost("Create")]
    public async Task<IActionResult> Create(ItemDTO req)
    {
        var result = await _servItem.Create(req);

        if (result == "No empty allow!")
            return BadRequest("Error: There are empty values, No empty values allow!");

        else if (result == "Database error!")
            return BadRequest("Error: Request to database failed!");

        else if (result == "No action!")
            return BadRequest("Error: item not created!");

        return Ok(result);
    }


    [HttpPut("Edit")]
    public async Task<IActionResult> Edit(ItemDTO req)
    {
        var result = await _servItem.Update(req);

        if (result == "No exist!")
            return BadRequest("Error: Client does not exist!");

        else if (result == "Database error!")
            return BadRequest("Error: Request to database failed!");

        else if (result == "No action!")
            return BadRequest("Error: item not edited!");

        

        return Ok(result);
    }


    [HttpPost("Remove")]
    public async Task<IActionResult> Remove(ItemDTO req)
    {
        var result = await _servItem.Delete(req.Id);

        if (result == "No empty allow!")
            return BadRequest("Error: There are empty values, No empty values allow!");

        else if (result == "Database error!")
            return BadRequest("Error: Request to database failed!");

        else if (result == "No action!")
            return BadRequest("Error: item not removed!");

        return Ok(result);
    }

}