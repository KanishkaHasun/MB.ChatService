using ChatService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user); 
        Task<User?> GetByIdAsync(Guid id);
    }
}
