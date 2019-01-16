using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MovieApplication.Core.Data;
using MovieApplication.Core.Repositories;
using MovieApplication.Core.Repositories.Interfaces;
using MovieApplication.Core.Service.Service.ServiceInterfaces;
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Core.Service.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMovieApplicationUnitOfWork _uow;

        public UserService(MovieApplicationDbContext dbContext)
        {
            _uow = new MovieApplicationUnitOfWork(dbContext);
        }

        public string GenerateToken(string username)
        {
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddHours(3);

            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)tokenHandler.CreateJwtSecurityToken(issuer: "movie.com", audience: "movie.com",
                    subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public async Task<string> Register(User user)
        {
            var userCheck = _uow.User.Get(x => x.UserName == user.UserName && x.Password == user.Password);

            if (userCheck == null)
                return null;

            await _uow.User.InsertAsync(user);

            var token = GenerateToken(user.UserName);
            var session = new Session
            {
                Token = token,
                UserId = user.Id,
                ExpiryDate = (Int32)(DateTime.Now.AddHours(3).Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
            };
            await _uow.Session.InsertAsync(session);
            _uow.Save();

            return token;
        }

        public async Task<string> Login(User user)
        {
            string token = "";
            var loggedInUser = _uow.User.Get(x => x.UserName == user.UserName && x.Password == user.Password);
            if (loggedInUser?.Result != null)
            {


                token = GenerateToken(user.UserName);
                var session = new Session
                {
                    Token = token,
                    UserId = user.Id,
                    ExpiryDate = (Int32)(DateTime.Now.AddHours(3).Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                };
                var currentSession = _uow.Session.GetList(x => x.UserId == loggedInUser.Id).Result
                    .OrderByDescending(x => x.ExpiryDate).FirstOrDefault();
                var sessionExists = currentSession?.ExpiryDate > (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                if (!sessionExists)
                {
                    await _uow.Session.InsertAsync(session);
                }
                else
                {
                    token = currentSession.Token;
                }

                _uow.Save();

            }

            return token;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _uow.User.Get(x => x.UserName == username.ToString());
        }
    }
}
