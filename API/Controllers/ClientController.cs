
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{

    private readonly IServiceClient _servClient;

    public ClientController(IServiceClient servClient)
    {
        _servClient = servClient;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ClientDTO>>> GetAll()
    {
        var clientDtos = await _servClient.GetAll();

        return clientDtos;
    }

    [AllowAnonymous]
    [HttpPost("Signup")]
    public async Task<IActionResult> Signup(ClientDTO req)
    {

        var result = await _servClient.Signup(req);

        if (result == "No empty allow!")
            return BadRequest("Error: There are empty values, No empty values allow!");

        else if (result == "Already exist!")
            return BadRequest("Error: Email already exists!");

        else if (result == "No action!")
            return BadRequest("Error: client not created!");

        else if (result == "Database error!")
            return BadRequest("Error: Request to database failed!");
       

        return Ok(result);

    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(ClientDTO req)
    {

        var result = await _servClient.Create(req);

        if (result == "No empty allow!")
            return BadRequest("Error: There are empty values, No empty values allow!");

        else if (result == "Already exist!")
            return BadRequest("Error: Email already exists!");

        else if (result == "No action!!")
            return BadRequest("Error: client not created!");

        else if (result == "Database error!")
            return BadRequest("Error: Request to database failed!");
       

        return Ok(result);
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login(ClientDTO req)
    {
        var result = await _servClient.Login(req.Email, req.Password);

        if (Convert.ToString(result) == "No empty allow!")
            return BadRequest("Error: There are empty values, No empty values allow!");

        else if (Convert.ToString(result) == "No exist!")
            return BadRequest("Error: Email does not exist!");

        else if (Convert.ToString(result) == "No password!")
            return BadRequest("Error: Password is incorrect!");

        else if (Convert.ToString(result) == "No action!")
            return BadRequest("Error: client not logged in!");

        else if (Convert.ToString(result) == "Database error!")
            return BadRequest("Error: Request to database failed!");

        // returns an object with token and client.
        return Ok(result);

    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout(ClientDTO req)
    {
        var result = await _servClient.Logout(req.Email);

        if (result == "No empty allow!")
            return BadRequest("Error: There are empty values, No empty values allow!");

        else if (result == "No exist!")
            return BadRequest("Error: Email does not exist!");

        else if (result == "No action!")
            return BadRequest("Error: client not logged out!");

        else if (result == "Database error!")
            return BadRequest("Error: Request to database failed!");

        return Ok(result);

    }


    [HttpPost("Signout")]
    public async Task<IActionResult> Signout(ClientDTO req)
    {
        var result = await _servClient.Delete(req.Email, req.Password);

        if (result == "No empty allow!")
            return BadRequest("Error: There are empty values, No empty values allow!");

        else if (result == "No exist!")
            return BadRequest("Error: Email does not exist!");

        else if (result == "No password!")
            return BadRequest("Error: Password is incorrect!");

        else if (result == "Database error!")
            return BadRequest("Error: Request to database failed!");

        else if (result == "No action!")
            return BadRequest("Error: client not deleted!");


        return Ok(result);
    }


    [HttpPut("Edit")]
    public async Task<IActionResult> Edit(ClientDTO req)
    {

        var result = await _servClient.Update(req);

        if (result == "No exist!")
            return BadRequest("Error: Client does not exist!");

        else if (result == "No action!")
            return BadRequest("Error: client not edited!");

        else if (result == "Database error!")
            return BadRequest("Error: Request to database failed!");

        return Ok(result);
    }

}


