using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Core.Service.Service.ServiceInterfaces
{
    public interface IUserService
    {
        string GenerateToken(string username);
        Task<string> Register(User user);
        Task<string> Login(User user);
        Task<User> GetUserByUsername(string username);
    }
}
