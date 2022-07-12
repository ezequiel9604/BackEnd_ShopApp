
using AutoMapper;
using Domain.Interfaces.Services;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Helpers;
using Domain.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class ServiceClient : IServiceClient
{

    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly IRepositoryClient _repClient;
    private readonly IRepositoryComment _repComment;
    private readonly IRepositoryAddress _repAddress;
    private readonly IRepositoryPhone _repPhone;

    private readonly IRepositoryWeakDomain<State> _repWeakStatus;
    private readonly IRepositoryWeakDomain<Currancy> _repWeakCurrancy;
    private readonly IRepositoryWeakDomain<Appearance> _repWeakAppearance;
    private readonly IRepositoryWeakDomain<Language> _repWeakLanguage;
    private readonly IRepositoryWeakDomain<ClientType> _repWeakClientType;

    public ServiceClient(
        IMapper mapper, 
        IConfiguration config, 
        IRepositoryClient repClient,
        IRepositoryComment repComment,
        IRepositoryAddress repAddress,
        IRepositoryPhone repPhone,
        IRepositoryWeakDomain<State> repWeakStatus,
        IRepositoryWeakDomain<Currancy> repWeakCurrancy,
        IRepositoryWeakDomain<Appearance> repWeakAppearance,
        IRepositoryWeakDomain<Language> repWeakLanguage,
        IRepositoryWeakDomain<ClientType> repWeakClientType)
    {
        _mapper = mapper;
        _config = config;
        _repClient = repClient;
        _repComment = repComment;
        _repAddress = repAddress;
        _repPhone = repPhone;
        _repWeakStatus = repWeakStatus;
        _repWeakCurrancy = repWeakCurrancy;
        _repWeakLanguage = repWeakLanguage;
        _repWeakAppearance = repWeakAppearance;
        _repWeakClientType = repWeakClientType;
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

                var state = await _repWeakStatus.GetById(cli.StateId);
                cdto.State = state.Name;

                var curr = await _repWeakCurrancy.GetById(cli.CurrancyId);
                cdto.Currancy = state.Name;

                var appea = await _repWeakAppearance.GetById(cli.AppearanceId);
                cdto.Appearance = appea.Name;

                var lang = await _repWeakLanguage.GetById(cli.LanguageId);
                cdto.Language = lang.Name;

                var type = await _repWeakClientType.GetById(cli.ClientTypeId);
                cdto.Type = type.Name;

                var addrs = await _repAddress.GetByClientId(cli.Id);
                cdto.Addresss = _mapper.Map<List<AddressDTO>>(addrs);

                var phos = await _repPhone.GetByClientId(cli.Id);
                cdto.Phones = _mapper.Map<List<PhoneDTO>>(phos);

                var comms = await _repComment.GetByClientId(cli.Id);
                cdto.Comments = _mapper.Map<List<CommentDTO>>(comms);

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

            if (!clientdto.Addresss.IsNullOrEmpty())
                client.Addresses = _mapper.Map<List<Address>>(clientdto.Addresss);

            if (!clientdto.Phones.IsNullOrEmpty())
                client.Phones = _mapper.Map<List<Phone>>(clientdto.Phones);

            client.ClientTypeId = (clientdto.Type == "normal") ? 1 : (clientdto.Type == "express") ? 2 : 1;
            client.AppearanceId = 1; // light
            client.LanguageId = 1; // english
            client.CurrancyId = 1; // dollars
            client.StateId = 3; // offline

            _repClient.Create(client);

            int affectedRows = await _repClient.SaveAllChanges();

            if (affectedRows > 0)
                return "Success!";

            return "No action!";

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
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

            if (clientdto.Addresss.IsNullOrEmpty())
                client.Addresses = _mapper.Map<List<Address>>(clientdto.Addresss);

            if (clientdto.Phones.IsNullOrEmpty())
                client.Phones = _mapper.Map<List<Phone>>(clientdto.Phones);

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


    public async Task<string> Delete(string email, string password)
    {

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return "No empty allow!";

        try
        {
            var client = await _repClient.GetByEmail(email);

            if (client is null)
                return "No exist!";

            var passCorrect = Passwords.VerifyPassword(password, client.PasswordHash, client.PasswordSalt);

            if (!passCorrect)
                return "No exist";


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
            cdto.Addresss = _mapper.Map<List<AddressDTO>>(client.Addresses);
            cdto.Phones = _mapper.Map<List<PhoneDTO>>(client.Phones);

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

            client.ClientTypeId = 1; // normal
            client.AppearanceId = 1; // light
            client.LanguageId = 1; // english
            client.CurrancyId = 1; // dollars
            client.StateId = 3; // offline

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


            client.StateId = 3;

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

    Task<string> IService<ClientDTO>.Delete(string obj)
    {
        throw new NotImplementedException();
    }
}