using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApplication.Core.Service.Service.ServiceInterfaces;
using MovieApplication.Domain.Dto.Models;
using MovieApplication.Helper;

namespace MovieApplication.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieIntegrationService _movieIntegrationService;
        private readonly IMovieService _movieService;

        public MovieController(IMovieIntegrationService movieIntegrationService, IMovieService movieService)
        {
            _movieIntegrationService = movieIntegrationService;
            _movieService = movieService;
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ResponseCache(Duration = 600, VaryByHeader = "*")]
        [TokenValidation]
        public async Task<Movie> Search([FromQuery]string query)
        {
            var movie = await _movieIntegrationService.Search(query);
            await _movieIntegrationService.InsertMovieToDbIfNotExists(movie);

            return movie;
        }
    }
}