using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApplication.Core.Repositories.Interfaces;
using MovieApplication.Core.Service.Service.ServiceInterfaces;
using MovieApplication.Domain.Dto.Models;
using MovieApplication.Helper;

namespace MovieApplication.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User login)
        {
            var token = await _userService.Login(login);

            Response.Cookies.Append("SessionToken", token);

            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Append("SessionToken", "new", options:new CookieOptions{Expires = DateTime.Now.AddDays(-1)});

            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult Register()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var token = await _userService.Register(user);
            Response.Cookies.Append("SessionToken", token.ToString());
            return View();
        }
    }
}