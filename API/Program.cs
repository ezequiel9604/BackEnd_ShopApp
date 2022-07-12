
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Data;
using Infrastructure.Mapper;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(opts => opts.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionString")
));

// adding jwt token authentication service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts => opts.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("AppSettings:TokenKey").Value)),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false
    });

builder.Services.AddScoped<IRepositoryClient, RepositoryClient>();
builder.Services.AddScoped<IRepositoryItem, RepositoryItem>();
builder.Services.AddScoped<IRepositoryOrder, RepositoryOrder>();
builder.Services.AddScoped<IRepositoryComment, RepositoryComment>();
builder.Services.AddScoped<IRepositoryImage, RepositoryImage>();
builder.Services.AddScoped<IRepositorySubitem, RepositorySubitem>();
builder.Services.AddScoped<IRepositoryPhone, RepositoryPhone>();
builder.Services.AddScoped<IRepositoryAddress, RepositoryAddress>();

builder.Services.AddScoped<IRepositoryWeakDomain<Category>, RepositoryCategory>();
builder.Services.AddScoped<IRepositoryWeakDomain<Brand>, RepositoryBrand>();
builder.Services.AddScoped<IRepositoryWeakDomain<State>, RepositoryState>();
builder.Services.AddScoped<IRepositoryWeakDomain<Currancy>, RepositoryCurrancy>();
builder.Services.AddScoped<IRepositoryWeakDomain<Language>, RepositoryLanguage>();
builder.Services.AddScoped<IRepositoryWeakDomain<Appearance>, RepositoryAppearance>();
builder.Services.AddScoped<IRepositoryWeakDomain<ClientType>, RepositoryClientType>();

builder.Services.AddScoped<IServiceClient, ServiceClient>();
builder.Services.AddScoped<IServiceItem, ServiceItem>();
builder.Services.AddScoped<IServiceOrder, ServiceOrder>();
builder.Services.AddScoped<IServiceComment, ServiceComment>();

//builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(c => c.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
}));

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMappersProfile());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();