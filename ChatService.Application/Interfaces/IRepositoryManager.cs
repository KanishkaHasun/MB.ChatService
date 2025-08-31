﻿namespace ChatService.Application.Interfaces
{
    public interface IRepositoryManager
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
