using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MovieApplication.Core.Service.Service.ServiceInterfaces;
using MovieApplication.Core.Service.Service.ServiceModels;
using RestSharp;

namespace MovieApplication.Core.Service.Service.Services
{
    public class OmdbService : IMovieIntegrationService
    {
        private readonly IConfiguration _configuration;

        public OmdbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<MovieOmdbResponse> Search(string title)
        {
            var baseUrl = _configuration["MovieDataSource:Default"];
            var apiKey = _configuration["MovieDataSource:ApiKey"];
            IRestClient client = new RestClient(baseUrl);
            IRestRequest restRequest = new RestRequest("/", Method.POST);
            restRequest.AddQueryParameter("apikey", apiKey);
            restRequest.AddQueryParameter("t", title);

            var restResponse =  client.Execute<MovieOmdbResponse>(restRequest);

            return restResponse.Data;
        }
    }
}
