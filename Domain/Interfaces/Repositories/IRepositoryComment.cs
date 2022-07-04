﻿
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryComment : IRepository<Comment>
{
    Task<Comment> GetById(string id);
}

