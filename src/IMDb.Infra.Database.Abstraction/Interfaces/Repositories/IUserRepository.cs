﻿using IMDb.Domain.Entities.Abstract;

namespace IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
public interface IUserRepository<T> where T : User 
{
    Task Create(T user, CancellationToken cancellationToken);
    Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken);
    Task<T> GetByEmail(string email, CancellationToken cancellationToken);
}