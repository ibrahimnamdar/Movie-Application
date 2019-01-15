using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        string GenerateToken(string username);
        Task<User> GetUserByToken(string token);
    }
}
