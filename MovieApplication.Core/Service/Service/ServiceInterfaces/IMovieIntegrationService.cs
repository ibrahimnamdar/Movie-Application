using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieApplication.Core.Service.Service.ServiceModels;

namespace MovieApplication.Core.Service.Service.ServiceInterfaces
{
    public interface IMovieIntegrationService
    {
        Task<MovieOmdbResponse> Search(string title);
    }
}
