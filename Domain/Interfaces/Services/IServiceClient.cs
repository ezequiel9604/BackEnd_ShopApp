
using Domain.DTOs;

namespace Domain.Interfaces.Services;

public interface IServiceClient : IService<ClientDTO>
{
    Task<object> Login(string email, string password);

    Task<string> Signup(ClientDTO obj);

    Task<string> Logout(string email);

}
