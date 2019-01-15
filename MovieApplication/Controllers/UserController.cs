using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApplication.Core.Repositories.Interfaces;
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IMovieApplicationUnitOfWork _uow;

        public UserController(IUserRepository userRepository, IMovieApplicationUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel login)
        {
            var user = _uow.User.FirstOrDefault(x => x.UserName == login.Username && x.Password == login.Password);
            if (user == null)
                return View("Error");
            var token = _userRepository.GenerateToken(login.Username);
            var session = new Session
            {
                Token = token,
                UserId = user.Id,
                ExpiryDate = (Int32)(DateTime.Now.AddDays(7).Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
            };
            await _uow.Session.InsertAsync(session);

            Response.Cookies.Append("SessionToken", token);
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var registeredUser = _uow.User.InsertAsync(user);
            var token = _userRepository.GenerateToken(user.UserName);
            var session = new Session
            {
                Token = token,
                UserId = user.Id,
                ExpiryDate = (Int32)(DateTime.Now.AddDays(7).Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
            };
            await _uow.Session.InsertAsync(session);

            Response.Cookies.Append("SessionToken", token);
            return View();
        }
    }
}