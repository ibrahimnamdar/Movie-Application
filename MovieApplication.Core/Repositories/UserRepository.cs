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
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Core.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IMovieApplicationUnitOfWork _uow;
        private readonly IConfiguration _config;

        public UserRepository(MovieApplicationDbContext dbContext)
        {
            _uow = new MovieApplicationUnitOfWork(dbContext);
        }

        public string GenerateToken(string username)
        {
            var symmetricKey = Convert.FromBase64String(_config["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            int expireInHours = 3;

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),

                Expires = now.AddHours(Convert.ToInt32(expireInHours)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public async Task<User> GetUserByToken(string token)
        {
            var userTokenEntity = await _uow.Session.GetList(x => x.Token == token && x.ExpiryDate >= (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
            if (!userTokenEntity.Any()) return null;
            var userToken = userTokenEntity.OrderByDescending(y => y.Id).FirstOrDefault();
            var user = await _uow.User.Get(x => x.Id == userToken.UserId);

            return user;
        }
    }
}
