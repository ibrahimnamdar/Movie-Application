using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieApplication.Domain.Dto.Models;
using MovieApplication.Domain.ServiceModels;

namespace MovieApplication.Core.Service.Service.ServiceInterfaces
{
    public interface IMovieIntegrationService
    {
        Task<Movie> Search(string title);
        Task<Movie> InsertMovieToDbIfNotExists(Movie movie);
    }
}
