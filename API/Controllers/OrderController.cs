
using Domain.Interfaces.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{

    private readonly IServiceOrder _servOrder;

    public OrderController(IServiceOrder servOrder)
    {
        _servOrder = servOrder;
    }


    [HttpGet("GetAll")]
    public async Task<ActionResult<List<OrderDTO>>> GetAll()
    {
        var orders = await _servOrder.GetAll();

        return orders;
    }

    [HttpGet("GetByClientId")]
    public async Task<ActionResult<List<OrderDTO>>> GetByClientId(string clientid)
    {
        var ordersByClient = await _servOrder.GetByClientId(clientid);

        return ordersByClient;
    }

}
