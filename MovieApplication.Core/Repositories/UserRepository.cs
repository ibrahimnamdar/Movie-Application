using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieApplication.Core.Data;
using MovieApplication.Core.Repositories.Interfaces;
using MovieApplication.Core.Service.Service.ServiceInterfaces;
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Core.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IMovieApplicationUnitOfWork _uow;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;


        public UserRepository(IUserService userService,MovieApplicationDbContext dbContext)
        {
            _userService = userService;
            _uow = new MovieApplicationUnitOfWork(dbContext);
        }

        

        public async Task<User> GetUserByToken(string token)
        {
            var userTokenEntity = await _uow.Session.GetList(x => x.Token == token && x.ExpiryDate >= (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
            if (!userTokenEntity.Any()) return null;
            var userToken = userTokenEntity.OrderByDescending(y => y.Id).FirstOrDefault();
            var user = await _uow.User.Get(x => x.Id == userToken.UserId);

            return user;
        }

        public async Task<string> Register(User user)
        {
            var token = await _userService.Register(user);
            return token;
        }

        public async Task<string> Login(User user)
        {
            var token = await _userService.Login(user);
            return token;
        }
    }
}
