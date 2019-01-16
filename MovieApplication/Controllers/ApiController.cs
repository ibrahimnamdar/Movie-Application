using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApplication.Core.Service.Service.ServiceInterfaces;
using MovieApplication.Domain.Dto.Models;
using MovieApplication.Helper;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MovieApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IMovieIntegrationService _movieIntegrationService;
        private readonly IUserService _userService;

        public ApiController(IMovieIntegrationService movieIntegrationService, IUserService userService)
        {
            _movieIntegrationService = movieIntegrationService;
            _userService = userService;
        }


        [HttpPost]
        [ResponseCache(Duration = 600, VaryByHeader = "*")]
        [TokenValidation]
        [Route("Search")]
        [SwaggerResponse(200, Type = typeof(Movie))]
        public async Task<Movie> Search([FromQuery] string query)
        {
            var movie = await _movieIntegrationService.Search(query);
            await _movieIntegrationService.InsertMovieToDbIfNotExists(movie);

            return movie;
        }

        [HttpPost]
        [Route("Login")]
        [SwaggerResponse(200, Type = typeof(string))]
        public async Task<string> Login(User user)
        {
            var token = await _userService.Login(user);
            return token;
        }
    }
}