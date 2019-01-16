using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MovieApplication.Core.Repositories.Interfaces;
using MovieApplication.Core.Service.Service.ServiceInterfaces;
using MovieApplication.Domain.Dto.Models;
using MovieApplication.Domain.ServiceModels;
using RestSharp;

namespace MovieApplication.Core.Service.Service.Services
{
    public class MovieService : IMovieService
    {
        private readonly IConfiguration _configuration;
        private readonly IMovieApplicationUnitOfWork _uow;

        public MovieService(IConfiguration configuration, IMovieApplicationUnitOfWork uow)
        {
            _configuration = configuration;
            _uow = uow;
        }

        public async Task<bool> UpdateAllMovies()
        {
            try
            {
                var allMovies = _uow.Movie.GetList().Result;
                var baseUrl = _configuration["MovieDataSource:Default"];
                var apiKey = _configuration["MovieDataSource:ApiKey"];
                IRestClient client = new RestClient(baseUrl);

                foreach (var movie in allMovies)
                {
                    IRestRequest restRequest = new RestRequest("/", Method.POST);
                    restRequest.AddQueryParameter("apikey", apiKey);
                    restRequest.AddQueryParameter("i", movie.ImdbId);

                    var restResponse = client.Execute<MovieOmdbResponse>(restRequest);

                    await _uow.Movie.Update(Mapper.Map<Movie>(restResponse.Data));

                }
                _uow.Save();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
