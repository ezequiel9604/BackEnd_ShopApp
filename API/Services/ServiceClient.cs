
using AutoMapper;
using Domain.Interfaces.Services;
using API.DTOs;
using Domain.Entities;
using API.Helpers;
using Domain.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class ServiceClient : IServiceClient
{

    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly IRepositoryClient _repClient;

    public ServiceClient(
        IMapper mapper, 
        IConfiguration config, 
        IRepositoryClient repClient)
    {
        _mapper = mapper;
        _config = config;
        _repClient = repClient;
    }

    public async Task<List<ClientDTO>> GetAll()
    {
        try
        {
            var clients = await _repClient.GetAll();

            if (clients is null)
                return new List<ClientDTO>();


            var clientDtos = new List<ClientDTO>();
            foreach (var cli in clients)
            {
                var cdto = _mapper.Map<ClientDTO>(cli);
                cdto.AddressDtos = _mapper.Map<List<AddressDTO>>(cli.Addresses).ToArray();
                cdto.PhoneDtos = _mapper.Map<List<PhoneDTO>>(cli.Phones).ToArray();

                clientDtos.Add(cdto);
            }

            return clientDtos;

        }
        catch (Exception)
        {
            return new List<ClientDTO>();
        }

    }


    public async Task<string> Create(ClientDTO clientdto)
    {

        if (string.IsNullOrEmpty(clientdto.FirstName) || string.IsNullOrEmpty(clientdto.LastName) ||
            string.IsNullOrEmpty(clientdto.Email) || string.IsNullOrEmpty(clientdto.Password) ||
            string.IsNullOrEmpty(clientdto.Genre) || string.IsNullOrEmpty(clientdto.ImagePath) ||
            string.IsNullOrEmpty(clientdto.Type))
        {
            return "No empty allow!";
        }

        try
        {
            // checking if email exist already
            if (await _repClient.GetByEmail(clientdto.Email) is not null)
                return "Already exist!";


            Passwords.CreatePassword(clientdto.Password, out byte[] hash, out byte[] salt);


            // creating an id to new client
            var randomId = "CLT-" + new Random().Next(1000, 9999);
            while (true)
            {
                if (await _repClient.GetById(randomId) is null)
                    break;

                randomId = "CLT-" + new Random().Next(1000, 9999);
            }

            var client = _mapper.Map<Client>(clientdto);
            client.Id = randomId;
            client.PasswordHash = hash;
            client.PasswordSalt = salt;

            if (!clientdto.AddressDTOs.IsNullOrEmpty())
                client.Addresses = _mapper.Map<List<Address>>(clientdto.AddressDTOs);

            if (!clientdto.PhoneDTOs.IsNullOrEmpty())
                client.Phones = _mapper.Map<List<Phone>>(clientdto.PhoneDTOs);

            client.ClientTypeId = (clientdto.Type == "normal") ? 1 : (clientdto.Type == "express") ? 2 : 1;
            client.AppearanceId = 1;
            client.LanguageId = 1;
            client.CurrancyId = 1;
            client.StateId = 2;

            _repClient.Create(client);

            int affectedRows = await _repClient.SaveAllChanges();

            if (affectedRows > 0)
                return "Success!";

            return "No action!";

        }
        catch (Exception)
        {
            return "Database error!";
        }

    }


    public async Task<string> Update(ClientDTO clientdto)
    {

        try
        {
            var client = await _repClient.GetById(clientdto.Id);

            if (client is null)
                return "No exist!";

            // whatchout this code
            if (!string.IsNullOrEmpty(clientdto.FirstName))
                client.FirstName = clientdto.FirstName;

            if (!string.IsNullOrEmpty(clientdto.LastName))
                client.LastName = clientdto.LastName;

            if (!string.IsNullOrEmpty(clientdto.Genre))
                client.Genre = clientdto.Genre;

            if (!string.IsNullOrEmpty(clientdto.ImagePath))
                client.ImagePath = clientdto.ImagePath;

            if (!string.IsNullOrEmpty(clientdto.Email))
                client.Email = clientdto.Email;

            if (!string.IsNullOrEmpty(clientdto.Password))
            {
                Passwords.CreatePassword(clientdto.Password, out byte[] hash, out byte[] salt);
                client.PasswordHash = hash;
                client.PasswordSalt = salt;
            }

            if (clientdto.AddressDTOs.IsNullOrEmpty())
                client.Addresses = _mapper.Map<List<Address>>(clientdto.AddressDTOs);

            if (clientdto.PhoneDTOs.IsNullOrEmpty())
                client.Phones = _mapper.Map<List<Phone>>(clientdto.PhoneDTOs);

            if (clientdto.YearOfBirth != 0 && clientdto.MonthOfBirth != 0 && clientdto.MonthOfBirth != 0)
                client.DateOfBirth = new DateTime(clientdto.YearOfBirth, clientdto.MonthOfBirth, clientdto.DayOfBirth);

            if (!string.IsNullOrEmpty(clientdto.Type))
                client.ClientTypeId = (clientdto.Type == "normal") ? 1 : (clientdto.Type == "express") ? 2 : 1;

            if (!string.IsNullOrEmpty(clientdto.Appearance))
                client.AppearanceId = (clientdto.Appearance == "light") ? 1 : (clientdto.Appearance == "dark") ? 2 : 1;

            if (!string.IsNullOrEmpty(clientdto.Language))
                client.LanguageId = (clientdto.Language == "english") ? 1 : (clientdto.Language == "spanish") ? 2 : 1;

            if (!string.IsNullOrEmpty(clientdto.Currancy))
                client.CurrancyId = (clientdto.Currancy == "usa / dollars") ? 1 : (clientdto.Currancy == "dom / pesos") ? 2 : 1;

            _repClient.Update(client);

            int affectedRows = await _repClient.SaveAllChanges();

            if (affectedRows > 0)
            {
                return "Success!";
            }

            return "No action!"; 

        }
        catch (Exception)
        {
            return "Database error!";
        }

    }


    public async Task<string> Delete(string email)
    {

        if (string.IsNullOrEmpty(email))
            return "No empty allow!";

        try
        {
            var client = await _repClient.GetByEmail(email);

            if (client is null)
                return "No exist!";

            _repClient.Delete(client);

            int affectedRows = await _repClient.SaveAllChanges();

            if (affectedRows > 0)
                return "Success!";

            return "No action!";

        }
        catch (Exception)
        {
            return "Database error!";
        }

    }


    public async Task<object> Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return "No empty allow!";

        try
        {
            var client = await _repClient.GetByEmail(email);

            if (client is null)
                return "No exist!";

            if (!Passwords.VerifyPassword(password, client.PasswordHash, client.PasswordSalt))
                return "No password!";

            client.StateId = 1;

            _repClient.Update(client);

            int affectedRows = await _repClient.SaveAllChanges();

            if (affectedRows == 0)
                return "No action"!;


            var token = Tokens.CreateToken(client, _config);

            var cdto = _mapper.Map<ClientDTO>(client);
            cdto.AddressDtos = _mapper.Map<List<AddressDTO>>(client.Addresses).ToArray();
            cdto.PhoneDtos = _mapper.Map<List<PhoneDTO>>(client.Phones).ToArray();

            return new { Token = token, User = cdto };

        }
        catch (Exception)
        {
            return "Database error!";
        }
    }


    public async Task<string> Signup(ClientDTO clientdto)
    {

        if (string.IsNullOrEmpty(clientdto.FirstName) || string.IsNullOrEmpty(clientdto.LastName) ||
            string.IsNullOrEmpty(clientdto.Email) || string.IsNullOrEmpty(clientdto.Password) ||
            string.IsNullOrEmpty(clientdto.Genre))
        {
            return "No empty allow!";
        }

        try
        {

            // checking if email exist already
            if (await _repClient.GetByEmail(clientdto.Email) is not null)
                return "Already exist!";

            Passwords.CreatePassword(clientdto.Password, out byte[] hash, out byte[] salt);

            // creating an id to new client
            var randomId = "CLT-" + new Random().Next(1000, 9999);
            while (true)
            {
                if (await _repClient.GetById(randomId) is null)
                    break;

                randomId = "CLT-" + new Random().Next(1000, 9999);
            }

            var client = _mapper.Map<Client>(clientdto);
            client.Id = randomId;
            client.PasswordHash = hash;
            client.PasswordSalt = salt;
            client.ImagePath = string.IsNullOrEmpty(clientdto.ImagePath) ?
                                "placeholder-man.png" : clientdto.ImagePath;
            client.AppearanceId = 1;
            client.LanguageId = 1;
            client.CurrancyId = 1;
            client.StateId = 2;
            client.ClientTypeId = 1;

            _repClient.Create(client);

            int affectedRows = await _repClient.SaveAllChanges();

            if (affectedRows > 0)
                return "Success!";

            return "No action!";

        }
        catch (Exception)
        {
            return "Database error!";
        }

    }


    public async Task<string> Logout(string email)
    {
        if (string.IsNullOrEmpty(email))
            return "No empty allow!";

        try
        {
            var client = await _repClient.GetByEmail(email);

            if (client is null)
                return "No exist!";


            client.StateId = 2;

            _repClient.Update(client);

            int affectedRows = await _repClient.SaveAllChanges();

            if (affectedRows > 0)
                return "Success!";

            return "No action!";

        }
        catch (Exception)
        {
            return "Database error!";
        }

    }

  
}