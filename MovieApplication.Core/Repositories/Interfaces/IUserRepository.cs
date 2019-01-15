using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<string> Login(User user);
        Task<string> Register(User user);
        Task<User> GetUserByToken(string token);
    }
}
