
using Domain.DTOs;

namespace Domain.Interfaces.Services;

public interface IServiceOrder : IService<OrderDTO>
{
    Task<List<OrderDTO>> GetByClientId(string clientId);
}
