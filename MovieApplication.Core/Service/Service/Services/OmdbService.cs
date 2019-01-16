using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using MovieApplication.Core.Repositories.Interfaces;
using MovieApplication.Core.Service.Service.ServiceInterfaces;
using MovieApplication.Domain.ServiceModels;
using MovieApplication.Domain;
using MovieApplication.Domain.Dto.Models;
using RestSharp;

namespace MovieApplication.Core.Service.Service.Services
{
    public class OmdbService : IMovieIntegrationService
    {
        private readonly IConfiguration _configuration;
        private readonly IMovieApplicationUnitOfWork _uow;

        public OmdbService(IConfiguration configuration, IMovieApplicationUnitOfWork uow)
        {
            _configuration = configuration;
            _uow = uow;
        }

        public async Task<Movie> Search(string title)
        {
            var baseUrl = _configuration["MovieDataSource:Default"];
            var apiKey = _configuration["MovieDataSource:ApiKey"];
            IRestClient client = new RestClient(baseUrl);
            IRestRequest restRequest = new RestRequest("/", Method.POST);
            restRequest.AddQueryParameter("apikey", apiKey);
            restRequest.AddQueryParameter("t", title);

            var restResponse = client.Execute<MovieOmdbResponse>(restRequest);

            return Mapper.Map<Movie>(restResponse.Data);
        }

        public async Task<Movie> InsertMovieToDbIfNotExists(Movie movie)
        {
            if (string.IsNullOrEmpty(movie?.ImdbId))
                return null;

            var movieExists = await _uow.Movie.Get(x => x.ImdbId == movie.ImdbId);
            if (movieExists == null)
            {
                await _uow.Movie.InsertAsync(movie);
                _uow.Save();
            }

            return movieExists;
        }
    }
}
